from Repository.Repository import Repository
from Service.HashTable import HashTable
import unittest

class HashTableTests(unittest.TestCase):
    def __init__(self):
        super().__init__()
        self.doctorHashTable = HashTable()
        self.createHashTable()

    def createHashTable(self):
        repository = Repository()
        self.data = repository.readDoctorDiseaseMapping()
        self.doctorHashTable.setValues(4, 2, self.data)
        self.doctorHashTable.buildHashtable()

    def testData(self):
        for i in self.data:
            self.assertEqual(self.doctorHashTable.getKey(i[0]), [i[1], i[2]])

    def runTests(self):
        self.testData()
        print("All test cases passed")