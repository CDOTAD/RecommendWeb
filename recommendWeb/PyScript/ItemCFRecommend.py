import math


def recommend_item_cf(user_item_list,similar_matrix,K):

    rank=dict()

    for item in user_item_list:

        for item_tuple in similar_matrix[item]:

            j=item_tuple[0]

            wj=item_tuple[1]

            if j in user_item_list:

                continue

            if j not in rank.keys():

                rank[j] = 0

            rank[j] += wj

    return sorted(rank.items(),key=lambda rank:rank[1],reverse=True)[0:K]





