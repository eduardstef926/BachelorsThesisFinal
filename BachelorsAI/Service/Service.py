import numpy as np
from scipy.stats import mode
import time
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.ensemble import RandomForestClassifier
from sklearn.preprocessing import LabelEncoder
from sklearn.model_selection import train_test_split, GridSearchCV
from sklearn.metrics import accuracy_score, confusion_matrix
from sklearn.tree import DecisionTreeClassifier


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
        param_grid = {
            'criterion': ['gini', 'entropy'],
            'splitter': ['best', 'random'],
            'max_depth': [2, 5, 10, None],
            'min_samples_split': [2, 5, 10],
            'min_samples_leaf': [1, 2, 4],
        }
        model = RandomForestClassifier(random_state=18)
        grid_search = GridSearchCV(model, param_grid=param_grid, cv=5)
        grid_search.fit(xTrain, yTrain)
        print("Best hyperparameters found by grid search:")
        print(grid_search.best_params_)
        predictions = grid_search.best_estimator_.predict(xTest)
        print(f"Accuracy on train data by Decision Tree Classifier: {accuracy_score(yTrain, grid_search.best_estimator_.predict(xTrain)) * 100}")
        print(f"Accuracy on test data by Decision Tree Classifier: {accuracy_score(yTest, predictions) * 100}")
        confusionMatrix = confusion_matrix(yTest, predictions)
        plt.figure(figsize=(12, 8))
        sns.heatmap(confusionMatrix, annot=True)
        plt.title("Confusion Matrix for Decision Tree Classifier on Test Data")
        plt.show()

        finalModel = grid_search.best_estimator_
        finalModel.fit(x, y)
        test_data = pd.read_csv(self.filePath).dropna(axis=1)
        testX = test_data.iloc[:, :-1]
        testY = encoder.transform(test_data.iloc[:, -1])
        finalPredictions = finalModel.predict(testX)
        print(f"Accuracy on Test dataset by the combined model: {accuracy_score(testY, finalPredictions) * 100}")
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
        print(f"Accuracy on train data by Decision Forest Classifier\: {accuracy_score(yTrain, model.predict(xTrain)) * 100}")
        print(f"Accuracy on test data by Decision Forest Classifier\: {accuracy_score(yTest, predictions) * 100}")
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

    from sklearn.model_selection import GridSearchCV

    # def trainAndTest(self):
    #     trainData = self.repository.getData()
    #     encoder = LabelEncoder()
    #     trainData["prognosis"] = encoder.fit_transform(trainData["prognosis"])
    #     x = trainData.iloc[:, :-1]
    #     y = trainData.iloc[:, -1]
    #     xTrain, xTest, yTrain, yTest = train_test_split(x, y, test_size=0.2, random_state=24)
    #     print(f"Train: {xTrain.shape}, {yTrain.shape}")
    #     print(f"Test: {xTest.shape}, {yTest.shape}")
    #
    #     # define the parameter grid for hyperparameter tuning
    #     param_grid = {
    #         'n_estimators': [50, 100, 150, 200],
    #         'max_depth': [2, 4, 6, 8],
    #         'min_samples_split': [2, 4, 6, 8],
    #         'min_samples_leaf': [1, 2, 4, 8],
    #     }
    #
    #     # create a random forest classifier object
    #     model = RandomForestClassifier(random_state=18)
    #
    #     # use grid search to find the best hyperparameters
    #     grid_search = GridSearchCV(model, param_grid, cv=5, verbose=1)
    #     grid_search.fit(xTrain, yTrain)
    #
    #     # get the best model from the grid search
    #     best_model = grid_search.best_estimator_
    #
    #     # evaluate the best model on the test set
    #     predictions = best_model.predict(xTest)
    #     print(
    #         f"Accuracy on train data by Decision Forest Classifier\: {accuracy_score(yTrain, best_model.predict(xTrain)) * 100}")
    #     print(f"Accuracy on test data by Decision Forest Classifier\: {accuracy_score(yTest, predictions) * 100}")
    #     confusionMatrix = confusion_matrix(yTest, predictions)
    #     plt.figure(figsize=(12, 8))
    #     sns.heatmap(confusionMatrix, annot=True)
    #     plt.title("Confusion Matrix for Random Forest Classifier on Test Data")
    #     plt.show()
    #
    #     # fit the best model on the entire dataset
    #     best_model.fit(x, y)
    #     test_data = pd.read_csv(self.filePath).dropna(axis=1)
    #     testX = test_data.iloc[:, :-1]
    #     testY = encoder.transform(test_data.iloc[:, -1])
    #     finalPredictions = best_model.predict(testX)
    #
    #     print(f"Accuracy on Test dataset by the combined model\: {accuracy_score(testY, finalPredictions) * 100}")
    #     confusionMatrix = confusion_matrix(testY, finalPredictions)
    #     plt.figure(figsize=(12, 8))
    #     sns.heatmap(confusionMatrix, annot=True)
    #     plt.title("Confusion Matrix for Combined Model on Test Dataset")
    #     plt.show()
    #
    #     symptoms = x.columns.values
    #     symptom_index = {}
    #     for index, value in enumerate(symptoms):
    #         symptom = " ".join([i.capitalize() for i in value.split("_")])
    #         symptom_index[symptom] = index
    #     dataDictionary = {
    #         "symptom_index": symptom_index,
    #         "predictions_classes": encoder.classes_
    #     }
    #     self.finalModel = best_model
    #     self.dataDictionary = dataDictionary

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

