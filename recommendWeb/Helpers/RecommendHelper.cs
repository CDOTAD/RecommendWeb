using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MySql.Data.MySqlClient;
using recommendWeb.Providers;
using recommendWeb.Models;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;


namespace recommendWeb.Helpers
{
    /// <summary>
    /// RecommendController的辅助类
    /// 调用python脚本进行UserCF和ItemCF推荐
    /// 返回推荐的电影信息
    /// MovieRecommend
    /// {"Movie":
    ///     {"MovieId":
    ///      "Title":
    ///      "Genres":
    ///     }
    /// "Similarity":
    /// }
    /// 
    /// 
    /// </summary>
    public class RecommendHelper
    {

        private const string srcPath = "D:\\projects\\recommendWeb\\RecommendWeb\\recommendWeb\\src\\";

        private const string pyScriptPath = "D:\\projects\\recommendWeb\\RecommendWeb\\recommendWeb\\PyScript\\";

        /// <summary>
        /// UserCF推荐
        /// 给用户id为user_id的用户推荐电影
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static ArrayList recommendUserCF(int user_id)
        {

            PythonDictionary pyDic = null;

            try
            {
                pyDic = getUserItem(user_id);
            }
            catch(Exception e)
            {
                throw (e);
            }

            string serverpath = pyScriptPath + "UserCFRecommend.py";
            ScriptRuntime pyRuntime = Python.CreateRuntime();
            ScriptEngine Engine = pyRuntime.GetEngine("python");

            ICollection<string> Paths = Engine.GetSearchPaths();

            Paths.Add("D:\\Anaconda2-32\\Lib");
            Paths.Add("D:\\Anaconda2-32\\DLLs");
            Paths.Add("D:\\Anaconda2-32\\Lib\\site-packages");

            Engine.SetSearchPaths(Paths);

            ScriptScope pyScope = Engine.CreateScope();

            FileStream readStream = new FileStream(srcPath + "user_similar_matrix\\user_similar_matrix.txt", FileMode.Open, FileAccess.Read, FileShare.Read);

            BinaryFormatter formatter = new BinaryFormatter();

            PythonDictionary similar_matrix = (PythonDictionary)formatter.Deserialize(readStream);

            readStream.Close();

            dynamic pyScript = Engine.ExecuteFile(serverpath, pyScope);

            IronPython.Runtime.List result = pyScript.recommend_user_cf(user_id, pyDic,similar_matrix,3);

            return getRecommendMovie(result);

        }
        
        /// <summary>
        /// ItemCF推荐
        /// 给用户id为user_id的用户推荐电影
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static ArrayList recommendItemCF(int user_id)
        {
            string serverpath = pyScriptPath+"ItemCFRecommend.py";
            ScriptRuntime pyRuntime = Python.CreateRuntime();
            ScriptEngine Engine = pyRuntime.GetEngine("python");

            ICollection<string> Paths = Engine.GetSearchPaths();

            Paths.Add("D:\\Anaconda2-32\\Lib");
            Paths.Add("D:\\Anaconda2-32\\DLLs");
            Paths.Add("D:\\Anaconda2-32\\Lib\\site-packages");

            Engine.SetSearchPaths(Paths);

            ScriptScope pyScope = Engine.CreateScope();

            dynamic pyScript = Engine.ExecuteFile(serverpath, pyScope);

            IronPython.Runtime.List user_item_list = getUserLikeItem(user_id);

            PythonDictionary similar_matrix = getItemSimilarityMatrix(user_item_list);


            IronPython.Runtime.List result = pyScript.recommend_item_cf(user_item_list,similar_matrix,3);

            return getRecommendMovie(result);
        }


        public static ArrayList getRecommend()
        {
            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select * from recommender";

            mySqlCommand.CommandText = sqlStr;
            mySqlCommand.Connection.OpenAsync();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            ArrayList recommendList = new ArrayList();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        int userId = reader.GetInt32(0);
                        string userCF = reader.GetString(1);
                        string itemCF = reader.GetString(2);

                        Recommend recommend = new Recommend();
                        recommend.UserId = userId;
                        recommend.UserCF = userCF;
                        recommend.ItemCF = itemCF;

                        recommendList.Add(recommend);

                    }
                }
            }
            catch(Exception e)
            {
                throw (e);
            }
            finally
            {
                mySqlCommand.Connection.CloseAsync();
            }

            return recommendList;

        }

        /// <summary>
        /// 用于测试
        /// </summary>
        /// <returns></returns>
        public static ArrayList test()
        {
            ArrayList returnList = new ArrayList();

            string serverpath = "D:\\projects\\recommandSystem\\UserCFRecommend.py";

            ScriptRuntime pyRuntime = Python.CreateRuntime();

            ScriptEngine Engine = pyRuntime.GetEngine("python");

            ICollection<string> Paths = Engine.GetSearchPaths();

            Paths.Add("D:\\Anaconda2-32\\Lib");
            Paths.Add("D:\\Anaconda2-32\\DLLs");
            Paths.Add("D:\\Anaconda2-32\\Lib\\site-packages");

            ScriptScope pyScope = Engine.CreateScope();

            //Engine.ImportModule("pandas");
            //Engine.ImportModule("math");
            //Engine.ImportModule("pickle");

            Engine.SetSearchPaths(Paths);




            IronPython.Runtime.PythonDictionary pyDic = new IronPython.Runtime.PythonDictionary();

            ArrayList A = new ArrayList();
            A.Add('a');
            A.Add('b');
            A.Add('d');

            ArrayList B = new ArrayList();
            B.Add('a');
            B.Add('c');

            ArrayList C = new ArrayList();
            C.Add('b');
            C.Add('e');

            ArrayList D = new ArrayList();
            D.Add('c');
            D.Add('d');
            D.Add('e');


            pyDic.Add('A', A);
            pyDic.Add('B', B);
            pyDic.Add('C', C);
            pyDic.Add('D', D);


            dynamic obj = Engine.ExecuteFile(serverpath, pyScope);

            IronPython.Runtime.List result = obj.recommmend_user_cf('A', pyDic, 3);



            for (int i = 0; i < result.Count; i++)
            {
                IronPython.Runtime.PythonTuple pySet = (IronPython.Runtime.PythonTuple)result.ElementAt(i);
                Console.WriteLine(pySet.ElementAt(0));
                Console.WriteLine(pySet.ElementAt(1));

                returnList.Add(pySet.ElementAt(0));
            }


            Console.WriteLine(result);

            

            return returnList;
        }

        /// <summary>
        /// 通过推荐列表中的movieId找到相关的电影的全部信息
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static ArrayList getRecommendMovie(IronPython.Runtime.List result)
        {
            ArrayList recommendMovie = new ArrayList();

            for (int i = 0; i < result.Count; i++)
            {
                PythonTuple pySet = result.ElementAt(i) as PythonTuple;

                int movieId = (int)pySet.ElementAt(0);

                RecommendMovie rdMovie = new RecommendMovie();
                rdMovie.Similarity = (double)pySet.ElementAt(1);
                try
                {
                    rdMovie.Movie = MovieHelper.GetMovieById(movieId);
                }
                catch (Exception e)
                {
                    throw (e);
                }

                recommendMovie.Add(rdMovie);
            }

            return recommendMovie;
        }

        /// <summary>
        /// recommend辅助函数
        /// 获取user_id和movie_id的关联字典
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        private static PythonDictionary getUserItem(int user_id)
        {
            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select userId,movieId from rating";

            mySqlCommand.CommandText = sqlStr;
            mySqlCommand.Connection.OpenAsync();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            PythonDictionary pyDic = new PythonDictionary();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        int userId = reader.GetInt32(0);
                        int movieId = reader.GetInt32(1);

                        if (!pyDic.Keys.Contains(userId))
                        {
                            pyDic.Add(userId, new ArrayList());
                        }

                        ArrayList subList = pyDic[userId] as ArrayList;
                        subList.Add(movieId);
                    }
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                mySqlCommand.Connection.CloseAsync();
            }

            return pyDic;
        }

        /// <summary>
        /// 获取用户ID为user_id相关联的全部电影的ID
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        private static IronPython.Runtime.List getUserLikeItem(int user_id)
        {
            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select movieId from rating where userId = ?us_id";

            mySqlCommand.CommandText = sqlStr;
            mySqlCommand.Parameters.AddWithValue("?us_id", user_id);

            mySqlCommand.Connection.OpenAsync();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            IronPython.Runtime.List pyItemList = new List();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        pyItemList.append(reader.GetInt32(0));
                    }
                }
            }
            catch(Exception e)
            {
                throw (e);
            }
            finally
            {
                mySqlCommand.Connection.CloseAsync();
            }

            return pyItemList;
        }

        /// <summary>
        /// pyItemList为与用户相关联的所有电影ID
        /// 找到这些电影与其他电影的相似矩阵，并拼接成一个总体的相似矩阵
        /// </summary>
        /// <param name="pyItemList"></param>
        /// <returns></returns>
        private static PythonDictionary getItemSimilarityMatrix(IronPython.Runtime.List pyItemList)
        {
            PythonDictionary pyDic = new PythonDictionary();

            for(int i = 0; i < pyItemList.Count; i++)
            {
                int itemId = (int)pyItemList.ElementAt(i);

                FileStream readStream = new FileStream(srcPath + "item_similar_matrix\\item"+itemId+"_similar_matrix.txt", FileMode.Open, FileAccess.Read, FileShare.Read);

                BinaryFormatter formatter = new BinaryFormatter();

                IronPython.Runtime.List similarityList = (List)formatter.Deserialize(readStream);

                readStream.Close();

                pyDic.Add(itemId, similarityList);

            }

            return pyDic;

        }
    }
}