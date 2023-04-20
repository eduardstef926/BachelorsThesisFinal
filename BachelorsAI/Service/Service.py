import numpy as np
from scipy.stats import mode
import time
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.ensemble import RandomForestClassifier
from sklearn.preprocessing import LabelEncoder
from sklearn.model_selection import train_test_split
from sklearn.metrics import accuracy_score, confusion_matrix

class Service:
    def __init__(self, repository, doctorHashTable):
        self.filePath = "Dataset/Testing.csv"
        self.repository = repository
        self.doctorHashTable = doctorHashTable
        self.finalModel = None
        self.dataDictionary = None
        self.trainAndTest()
        self.hashData()

    def trainAndTest(self):
        trainData = self.repository.getData()
        encoder = LabelEncoder()
        trainData["prognosis"] = encoder.fit_transform(trainData["prognosis"])
        x = trainData.iloc[:, :-1]
        y = trainData.iloc[:, -1]
        xTrain, xTest, yTrain, yTest = train_test_split(x, y, test_size=0.2, random_state=24)
        print(f"Train: {xTrain.shape}, {yTrain.shape}")
        print(f"Test: {xTest.shape}, {yTest.shape}")

        model = RandomForestClassifier(random_state=18)
        model.fit(xTrain, yTrain)
        predictions = model.predict(xTest)
        print(f"Accuracy on train data by Random Forest Classifier\: {accuracy_score(yTrain, model.predict(xTrain)) * 100}")
        print(f"Accuracy on test data by Random Forest Classifier\: {accuracy_score(yTest, predictions) * 100}")
        confusionMatrix = confusion_matrix(yTest, predictions)
        plt.figure(figsize=(12, 8))
        sns.heatmap(confusionMatrix, annot=True)
        plt.title("Confusion Matrix for Random Forest Classifier on Test Data")
        plt.show()
        finalModel = RandomForestClassifier(random_state=18)
        finalModel.fit(x, y)
        test_data = pd.read_csv(self.filePath).dropna(axis=1)
        testX = test_data.iloc[:, :-1]
        testY = encoder.transform(test_data.iloc[:, -1])
        finalPredictions = finalModel.predict(testX)

        print(f"Accuracy on Test dataset by the combined model\: {accuracy_score(testY, finalPredictions) * 100}")
        confusionMatrix = confusion_matrix(testY, finalPredictions)
        plt.figure(figsize=(12, 8))
        sns.heatmap(confusionMatrix, annot=True)
        plt.title("Confusion Matrix for Combined Model on Test Dataset")
        plt.show()

        symptoms = x.columns.values
        symptom_index = {}
        for index, value in enumerate(symptoms):
            symptom = " ".join([i.capitalize() for i in value.split("_")])
            symptom_index[symptom] = index
        dataDictionary = {
            "symptom_index": symptom_index,
            "predictions_classes": encoder.classes_
        }
        self.finalModel = finalModel
        self.dataDictionary = dataDictionary

    def getDataDictionary(self):
        return list(self.dataDictionary["symptom_index"].keys())

    def predictDisease(self, symptoms):
        symptoms = symptoms.split(",")
        input_data = [0] * len(self.dataDictionary["symptom_index"])
        for symptom in symptoms:
            index = self.dataDictionary["symptom_index"][symptom]
            input_data[index] = 1
        input_data = np.array(input_data).reshape(1, -1)
        prediction = self.dataDictionary["predictions_classes"][self.finalModel.predict(input_data)[0]]
        return prediction


    def hashData(self):
        data = self.repository.readDoctorDiseaseMapping()
        self.doctorHashTable.setValues(2, 2, data)
        self.doctorHashTable.buildHashtable()


    def getKey(self, disease):
        return self.doctorHashTable.getKey(disease)


    def getInformationBySymptoms(self, symptoms):
        disease = self.predictDisease(symptoms)
        startTime = time.perf_counter()
        keyValue = self.doctorHashTable.getKey(disease)
        doctorTitle, doctorSpecialization = keyValue[0], keyValue[1]
        endTime = time.perf_counter()
        print("KeyLookupTime:", (endTime - startTime) * 1000)
        dataDictionary = {
            "diseaseName": disease,
            "doctorSpecialization": doctorSpecialization,
            "doctorTitle": doctorTitle
        }
        return dataDictionary

