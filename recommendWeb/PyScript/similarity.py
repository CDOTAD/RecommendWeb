import math


def item_similarity_one(data_set,item_id):

    target_item = set(data_set[item_id])

    similarity_matrix=dict()

    for item in data_set.keys():

        if item == item_id:

            continue

        user_set=set(data_set[item])

        similarity = float(len(user_set & target_item)) / (len(user_set)*len(target_item))

        similarity_matrix[item]=similarity

    similarity_matrix = sorted(similarity_matrix.items(),key=lambda similarity_matrix:similarity_matrix[1],reverse=True)[0:20]

    return similarity_matrix


def user_similar(data_set):
    train = data_set

    #print data_set

    item_users = dict()

    for u, items in train.items():

        for i in items:

            if i not in item_users:
                item_users[i] = set()

            item_users[i].add(u)

    #print(item_users)

    scale_matrix = dict()

    neighbor_matrix = dict()

    for i, users in item_users.items():

        for u in users:

            if u not in neighbor_matrix:
                neighbor_matrix[u] = 0

            neighbor_matrix[u] += 1

            for v in users:

                if u not in scale_matrix:
                    scale_matrix[u] = dict()

                if v not in scale_matrix[u]:
                    scale_matrix[u][v] = 0

                if u == v:
                    continue

                scale_matrix[u][v] += 1

    #print(scale_matrix)

    similar_matrix = dict()

    for u, related_users in scale_matrix.items():

        similar_matrix[u] = dict()

        for v, cuv in related_users.items():
            similar_matrix[u][v] = cuv / math.sqrt(neighbor_matrix[u] * neighbor_matrix[v])

    print similar_matrix

    return similar_matrix


#coon=pymysql.connect(host="localhost",user="root",password="123456",db="recommendsystem")

#cur=coon.cursor()

#cur.execute('select userId,movieId from rating')

#data=cur.fetchall()

#cur.close()

#coon.close()

#myData=dict()

#for item in data:

    #if item[1] not in myData.keys():

        #myData[item[1]]=set()

    #myData[item[1]].add(item[0])

#print myData

#print item_similarity_one(myData,1)

#aData=dict()

#aData['A']=set(['a','b','d'])
#aData['B']=set(['a','c'])
#aData['C']=set(['b','e'])
#aData['D']=set(['c','d','e'])

#rank = recommmend_user_cf(user_recommend='A',data_set=aData,k=3)

#print rank