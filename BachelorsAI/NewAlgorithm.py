import numpy as np
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.tree import DecisionTreeClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn.ensemble import GradientBoostingClassifier
from sklearn.metrics import f1_score

if __name__ == '__main__':
    df = pd.read_csv("Dataset/OldTraining.csv")
    df.isna().sum().value_counts()
    df.drop('Unnamed: 133',axis=1,inplace=True)
    X = df.iloc[:,:-1]
    y = df.iloc[:,-1]
    X.head()
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=69)
    decision = DecisionTreeClassifier().fit(X_train,y_train)
    decision.score(X_test,y_test)
    print(f1_score(decision.predict(X_test), y_test, average='micro'))