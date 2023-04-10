import numpy as np

class Service:
    def __init__(self, testing):
        self.testing = testing

    def testDataset(self):
        return self.testing.testOnDataset()

    def predictDisease(self, symptoms):
        data_dict = self.testing.testOnDataset()
        encoder, finalModel = self.testing.trainModel()
        symptoms = symptoms.split(",")
        inputData = [0] * len(data_dict["symptom_index"])
        for symptom in symptoms:
            index = data_dict["symptom_index"][symptom]
            inputData[index] = 1
        inputData = np.array(inputData).reshape(1, -1)
        prediction = data_dict["predictions_classes"][finalModel.predict(inputData)[0]]
        return prediction

