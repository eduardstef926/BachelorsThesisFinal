import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
import plotly.express as px
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
from sklearn.metrics import confusion_matrix
import numpy

def preprocess_inputs(df):
    df = df.copy()
    df = df.drop('Unnamed: 133', axis=1)
    y = df['prognosis']
    X = df.drop('prognosis', axis=1)
    X_train, X_test, y_train, y_test = train_test_split(X, y, train_size=0.7, shuffle=True, random_state=1)
    return X_train, X_test, y_train, y_test


if __name__ == '__main__':
    data = pd.read_csv("Dataset/Training.csv")
    X_train, X_test, y_train, y_test = preprocess_inputs(data)
    model = LogisticRegression()
    model.fit(X_train, y_train)
    print("Test Accuracy: {:.2f}%".format(model.score(X_test, y_test) * 100))


    coefficients = np.mean(model.coef_, axis=0)
    importance_threshold = np.quantile(np.abs(coefficients), q=0.25)
    fig = px.bar(
        x=coefficients,
        y=X_train.columns,
        orientation='h',
        color=coefficients,
        color_continuous_scale=[(0, 'red'), (1, 'blue')],
        labels={'x': "Coefficient Value", 'y': "Feature"},
        title="Feature Importance From Model Weights"
    )

    fig.add_vline(x=importance_threshold, line_color='yellow')
    fig.add_vline(x=-importance_threshold, line_color='yellow')
    fig.add_vrect(x0=importance_threshold, x1=-importance_threshold, line_width=0, fillcolor='yellow', opacity=0.2)
    fig.show()

    low_importance_features = X_train.columns[np.abs(coefficients) < importance_threshold]
    reduced_data = data.drop(low_importance_features, axis=1).copy()
    X_train, X_test, y_train, y_test = preprocess_inputs(reduced_data)
    reduced_data_model = LogisticRegression()
    reduced_data_model.fit(X_train, y_train)
    print("Test Accuracy: {:.2f}%".format(reduced_data_model.score(X_test, y_test) * 100))

    y_pred = reduced_data_model.predict(X_test)
    cm = confusion_matrix(y_test, y_pred)

    plt.figure(figsize=(30, 30))
    sns.heatmap(cm, annot=True, fmt='g', vmin=0, cmap='Blues', cbar=False)
    plt.xticks(np.arange(32) + 0.5, reduced_data_model.classes_, rotation=90)
    plt.yticks(np.arange(32) + 0.5, reduced_data_model.classes_, rotation=0)
    plt.xlabel("Predicted")
    plt.ylabel("Actual")
    plt.title("Confusion Matrix")
    plt.show()