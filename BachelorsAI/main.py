from Controller.Controller import Controller
from DatasetReader.DatasetReader import DatasetReader
from Service.Service import Service
from Testing.Testing import Testing
from Training.Training import Training

if __name__ == '__main__':
    dataset = DatasetReader()
    training = Training(dataset)
    testing = Testing(training)
    service = Service(testing)
    controller = Controller(service)
    controller.runServer()
