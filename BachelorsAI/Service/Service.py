import numpy
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.tree import DecisionTreeClassifier
from sklearn.preprocessing import LabelEncoder
from sklearn.model_selection import train_test_split
from sklearn.metrics import accuracy_score, confusion_matrix

class Service:
    def __init__(self, repository, doctorHashTable):
        self.filePath = "Dataset/UpdatesTraining.csv"
        self.repository = repository
        self.doctorHashTable = doctorHashTable
        self.diagnostic = {}
        self.createHashTable()
        self.finalModel = None
        self.dataDictionary = None

    def trainAndTest(self, k=5):
        trainData = self.repository.getData()
        encoder = LabelEncoder()
        trainData["prognosis"] = encoder.fit_transform(trainData["prognosis"])
        x = trainData.iloc[:, :-1]
        y = trainData.iloc[:, -1]

        model = DecisionTreeClassifier(random_state=18)
        xTrain, xTest, yTrain, yTest = train_test_split(x, y, test_size=0.2, random_state=24)
        model.fit(xTrain, yTrain)
        predictions = model.predict(xTest)
        print(f"Accuracy on train data: {accuracy_score(yTrain, model.predict(xTrain)) * 100}%")
        print(f"Accuracy on test data: {accuracy_score(yTest, predictions) * 100}%")
        confusionMatrix = confusion_matrix(yTest, predictions)
        plt.figure(figsize=(12, 8))
        sns.heatmap(confusionMatrix, annot=True)
        plt.title("Confusion Matrix for Test Data")
        plt.show()

        x_new = trainData.drop('prognosis', axis=1)
        y_new = trainData.prognosis
        x_train_new, x_test_new, y_train_new, y_test_new = train_test_split(x_new, y_new, test_size=0.3, random_state=42)
        model.fit(x_train_new, y_train_new)
        acc_new = model.score(x_test_new, y_test_new)
        print("Accuracy on test set: {:.2f}%".format(acc_new * 100))

        symptoms = x.columns.values
        symptom_index = {}
        for index, value in enumerate(symptoms):
            symptom = " ".join([i.capitalize() for i in value.split("_")])
            symptom_index[symptom] = index
        dataDictionary = {
            "symptom_index": symptom_index,
            "predictions_classes": encoder.classes_
        }
        self.finalModel = model
        self.dataDictionary = dataDictionary

    def predictProbabilities(self, symptoms):
        symptoms = symptoms.split(",")
        input_data = [0] * len(self.dataDictionary["symptom_index"])
        probabilities = self.finalModel.predict_proba([symptoms])
        disease_names = self.dataDictionary['predictions_classes']

        plt.figure(figsize=(10, 6))
        for i, disease in enumerate(disease_names):
            plt.bar(disease, probabilities[0][i], label=disease)
        plt.xlabel('Disease')
        plt.ylabel('Probability')
        plt.title('Probability Estimates for Diseases')
        plt.legend()
        plt.show()


    def getDataDictionary(self):
        return list(self.dataDictionary["symptom_index"].keys())

    def predictDisease(self, symptoms):
        symptoms = symptoms.split(",")
        input_data = [0] * len(self.dataDictionary["symptom_index"])
        for symptom in symptoms:
            index = self.dataDictionary["symptom_index"][symptom]
            input_data[index] = 1
        input_data = numpy.array(input_data).reshape(1, -1)
        prediction = self.dataDictionary["predictions_classes"][self.finalModel.predict(input_data)[0]]
        return prediction


    def hashData(self):
        data = self.repository.readDoctorDiseaseMapping()
        self.doctorHashTable.setValues(2, 2, data)
        self.doctorHashTable.buildHashtable()


    def getKey(self, disease):
        return self.doctorHashTable.getKey(disease)

    def createHashTable(self):
        data = pd.read_csv('../Dataset/oldTraining.csv')
        diagnostic = {}
        symptoms = list(data.columns)
        for k in symptoms:
            column = data[k]
            for i in range(0, len(column)):
                if column[i] == 1:
                    if k not in diagnostic.keys():
                        diagnostic[k] = [data.iat[i, -2]]
                    elif data.iat[i, -2] not in diagnostic[k]:
                        values = diagnostic[k]
                        values.append(data.iat[i, -2])
        self.diagnostic = diagnostic

    def getInformationBySymptoms(self, inputSymptoms):
        symptomsSplitList = inputSymptoms.split(',')
        appearances = {}
        for i in symptomsSplitList:
            obtainedSymptom = i.replace(' ', '_')
            obtainedSymptom = obtainedSymptom.lower()
            for j in self.diagnostic[obtainedSymptom]:
                if j not in appearances.keys():
                    appearances[j] = 1
                else:
                    values = appearances[j]
                    values += 1
                    appearances[j] = values
        maximum = -1
        diagnostic = ""
        for i in appearances.keys():
            if appearances[i] > maximum:
                diagnostic = i
                maximum = appearances[i]
        return diagnostic

