import time


class HashTable:

    def __init__(self):
        self.maximumKicking = 0
        self.hashTableCount = 0
        self.hashTable = []
        self.keyValuePairs = []

    def setValues(self, maximumKicking, hashTableCount, keyValuePairs):
        self.maximumKicking = maximumKicking
        self.hashTableCount = hashTableCount
        self.hashTable = [[float('inf') for i in range(len(keyValuePairs))] for j in range(2)]
        self.keyValuePairs = keyValuePairs

    def initTable(self):
        for i in range(self.hashTableCount):
            for j in range(self.maximumKicking):
                self.hashTable[i][j] = float('inf')

    @staticmethod
    def jenkins(s):
        hash = 0
        for c in s:
            hash += ord(c)
            hash += (hash << 10)
            hash ^= (hash >> 6)
        hash += (hash << 3)
        hash ^= (hash >> 11)
        hash += (hash << 15)
        return hash

    @staticmethod
    def elfHash(word):
        h = 0
        for c in word:
            h = (h << 4) + ord(c)
            g = h & 0xF0000000
            if g != 0:
                h ^= g >> 24
            h &= ~g
        return h

    @staticmethod
    def djb2Hash(input_str):
        hash = 5381
        for char in input_str:
            hash = ((hash << 5) + hash) + ord(char)
        return hash

    @staticmethod
    def fnv1Hash(input_str):
        hash_val = 0x811c9dc5
        prime = 0x01000193
        for char in input_str:
            hash_val = (hash_val * prime) & 0xffffffff
            hash_val ^= ord(char)
        return hash_val

    def hash(self, function, key):
        if function == 0:
            return self.djb2Hash(key) % len(self.hashTable[0])
        elif function == 1:
            return self.fnv1Hash(key) % len(self.hashTable[0])
        elif function == 2:
            return self.jenkins(key) % len(self.hashTable[0])
        elif function == 3:
            return self.elfHash(key) % len(self.hashTable[0])

    #   Complexity of lookup is ~O(1) on worst case as well
    def getKey(self, key):
        hashes = []
        for i in range(self.maximumKicking):
            hashes.append(self.hash(i, key))
        for i in range(self.maximumKicking):
            j = 0 if i % 2 == 0 else 1
            if self.hashTable[j][hashes[i]] != float('inf'):
                if self.hashTable[j][hashes[i]][0] == key:
                    return self.hashTable[j][hashes[i]][1]
        return None

    def placeKey(self, key, value, tableId):
        count = 0
        while count < self.maximumKicking:
            hashPosition = self.hash(count, key)
            if self.hashTable[tableId][hashPosition] != float('inf'):
                oldValue = self.hashTable[tableId][hashPosition]
                self.hashTable[tableId][hashPosition] = (key, value)
                key, value = oldValue[0], oldValue[1]
                tableId = 0 if tableId + 1 >= self.hashTableCount else tableId + 1
                count += 1
            else:
                self.hashTable[tableId][hashPosition] = (key, value)
                break
        return count

    def reHashKeys(self):
        i = 0
        while i < len(self.keyValuePairs):
            value = self.keyValuePairs[i]
            count = self.placeKey(value[0], [value[1], value[2]], 0)
            if count == self.maximumKicking:
                self.hashTable = [[float('inf') for i in range(2 * len(self.hashTable[0]))] for j in range(2)]
                i = 0
            else:
                i += 1

    def getHashTable(self):
        return self.hashTable

    def print_table(self):
        print("Final hash tables:")
        for i in range(self.hashTableCount):
            for j in range(len(self.hashTable[0])):
                if self.hashTable[i][j] == float('inf'):
                    print("- ", end="")
                else:
                    print(f"{self.hashTable[i][j]} ", end="")

    def buildHashtable(self):
        self.initTable()
        startTime = time.perf_counter()
        self.reHashKeys()
        endTime = time.perf_counter()
        print("Hashing Time", (endTime - startTime)*1000, "seconds")
