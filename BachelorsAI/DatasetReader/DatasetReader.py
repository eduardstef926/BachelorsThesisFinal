import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns

class DatasetReader:

    def __init__(self):
        self.filePath = "Dataset/Training.csv"
        self.data = {}
        self.readData()

    def readData(self):
        self.data = pd.read_csv(self.filePath).dropna(axis=1)
        self.plotData()

    def plotData(self):
        disease_counts = self.data["prognosis"].value_counts()
        temp_df = pd.DataFrame({
            "Disease": disease_counts.index,
            "Counts": disease_counts.values
        })
        plt.figure(figsize=(18, 8))
        sns.barplot(x="Disease", y="Counts", data=temp_df)
        plt.xticks(rotation=90)
        plt.show()

    def getData(self):
        return self.data
