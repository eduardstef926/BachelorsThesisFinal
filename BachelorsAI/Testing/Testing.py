import pandas as pd
from sklearn.metrics import accuracy_score

class Testing:
    def __init__(self, training):
        self.training = training

    def trainModel(self):
        return self.training.trainOnModel()

    def testOnDataset(self):
        encoder, finalModel = self.training.trainOnModel()
        test_data = pd.read_csv("./Dataset/Testing.csv").dropna(axis=1)
        xTest = test_data.iloc[:, :-1]
        yTest = encoder.transform(test_data.iloc[:, -1])
        predictions = finalModel.predict(xTest)

        print(f"Accuracy on Regular Tree Prediction\: {accuracy_score(yTest, predictions)*100}")

        symptoms = xTest.columns.values
        symptom_index = {}
        for index, value in enumerate(symptoms):
            symptom = " ".join([i.capitalize() for i in value.split("_")])
            symptom_index[symptom] = index

        dataDictionary = {
            "symptom_index": symptom_index,
            "predictions_classes": encoder.classes_
        }
        return dataDictionary