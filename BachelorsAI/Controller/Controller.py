from flask import Flask
from Repository.Repository import Repository
from Service.HashTable import HashTable
from Service.Service import Service

if __name__ == '__main__':
    server = Flask(__name__)
    repository = Repository()
    doctorHashTable = HashTable()
    service = Service(repository, doctorHashTable)
    print(service.getInformationBySymptoms("Itching,Skin Rash,Swelling"))


# @server.route('/getInformationBySymptoms/<symptoms>')
# def getInformationBySymptoms(symptoms):
#     return service.getInformationBySymptoms(symptoms)
#
#
# @server.route('/getAllSymptoms')
# def getAllSymptoms():
#     return service.getDataDictionary()
#
#
# def runServer():
#     server.run()
