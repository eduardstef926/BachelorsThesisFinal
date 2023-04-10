import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.preprocessing import LabelEncoder
from sklearn.model_selection import train_test_split
from sklearn.metrics import accuracy_score, confusion_matrix
from sklearn.tree import DecisionTreeClassifier

class Training:

    def __init__(self, dataset):
        self.dataset = dataset

    def trainOnModel(self):
        encoder = LabelEncoder()
        data = self.dataset.getData()
        data["prognosis"] = encoder.fit_transform(data["prognosis"])
        xData = data.iloc[:,:-1]
        yData = data.iloc[:, -1]
        xTrain, xTest, yTrain, yTest = train_test_split(xData, yData, test_size = 0.2, random_state = 24)
        temporaryModel = DecisionTreeClassifier()
        temporaryModel.fit(xTrain, yTrain)
        predictions = temporaryModel.predict(xTest)

        print(f"Accuracy on train data by Regular Tree Classifier\: {accuracy_score(yTrain, temporaryModel.predict(xTrain))*100}")
        print(f"Accuracy on test data by Regular Tree Classifier\: {accuracy_score(yTest, predictions)*100}")

        cf_matrix = confusion_matrix(yTest, predictions)
        plt.figure(figsize=(12,8))
        sns.heatmap(cf_matrix, annot=True)
        plt.title("Confusion Matrix for Regular Tree Classifier on Test Data")
        plt.show()

        finalModel = DecisionTreeClassifier()
        finalModel.fit(xData, yData)
        return encoder, finalModel