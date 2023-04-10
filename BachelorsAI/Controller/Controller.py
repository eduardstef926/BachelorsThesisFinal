from flask import Flask

server = Flask(__name__)

class Controller:

    def __init__(self, service):
        self.service = service

    @server.route('/getDiseaseBySymptoms/<symptoms>')
    def getDiseaseBySymptoms(self, symptoms):
        return self.service.predictDisease(symptoms)

    @server.route('/getAllSymptoms')
    def getAllSymptoms(self):
        return list(self.service.testDataset()["symptom_index"].keys())

    def runServer(self):
        server.run()
