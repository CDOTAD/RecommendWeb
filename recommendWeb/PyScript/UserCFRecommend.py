import math


def recommend_user_cf(user_recommend,data_set,similar_matrix,k=10):

    rank = dict()

    interacted_item = data_set[user_recommend]

    user_similarity = similar_matrix[user_recommend]

    for v, wuv in sorted(user_similarity.items(), key=lambda user_similarity: user_similarity[1], reverse=True)[0:k]:

        for i in data_set[v]:

            if i in interacted_item:
                continue

            if i not in rank:
                rank[i] = 0

            rank[i] += wuv

    rank = sorted(rank.items(), key=lambda rank: rank[1], reverse=True)[0:k]

    return rank






