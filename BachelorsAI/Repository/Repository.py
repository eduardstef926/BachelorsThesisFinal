import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
import csv

class Repository:
    def __init__(self):
        self.trainingFilePath = "Dataset/Training.csv"
        self.treatmentFilePath = "Dataset/Treatment.csv"
        self.trainingData = {}
        self.readSymptomDiseaseMapping()

    def readSymptomDiseaseMapping(self):
        self.trainingData = pd.read_csv(self.trainingFilePath).dropna(axis=1)

    def readDoctorDiseaseMapping(self):
        data = []
        with open(self.treatmentFilePath, 'r') as file:
            reader = csv.reader(file)
            for row in reader:
                elements = row[0].split(' ')
                data.append(elements)
        return data

    def plotData(self):
        disease_counts = self.trainingData["prognosis"].value_counts()
        temp_df = pd.DataFrame({
            "Disease": disease_counts.index,
            "Counts": disease_counts.values
        })
        plt.figure(figsize=(18, 8))
        sns.barplot(x="Disease", y="Counts", data=temp_df)
        plt.xticks(rotation=90)
        plt.show()

    def getData(self):
        return self.trainingData
