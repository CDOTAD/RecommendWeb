using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.ViewModel;
using System.Collections;
using DataModel;

namespace DataIteraction
{
    public class UserHelper
    {
        
        public static ArrayList GetAllUser()
        {
            recommendsystemEntities rse = new recommendsystemEntities();

            var query = from u in rse.user select new { u.userId };

            ArrayList userList = new ArrayList();

            foreach(var u in query)
            {
                UserView userView = new UserView();
                userView.UserId = u.userId;

                userList.Add(userView);
            }

            return userList;
        }

        public static UserView GetUserById(int id)
        {
            recommendsystemEntities res = new recommendsystemEntities();

            UserView userView = new UserView();

            var result = from u in res.user where u.userId == id select new { u.userId };

            ArrayList userList = new ArrayList();

            foreach(var u in result)
            {
  
                userView.UserId = u.userId;

               
            }

            return userView;
             
        }

        public static ArrayList GetUserLimit()
        {
            recommendsystemEntities res = new recommendsystemEntities();

            ArrayList limitUser = new ArrayList();

            var result = (from u in res.user select new { u.userId }).Take(15);

            foreach(var u in result)
            {
                UserView userView = new UserView();
                userView.UserId = u.userId;

                limitUser.Add(userView);
            }

            return limitUser;
        }

        public static int GetUserTotal()
        {
            recommendsystemEntities res = new recommendsystemEntities();

            return res.user.Count();
        }

    }
}
