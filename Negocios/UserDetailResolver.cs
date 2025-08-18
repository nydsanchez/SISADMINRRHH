using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocios
{
    public class UserDetailResolver
    {
        private static IUserDetail userDetail;

        public static IUserDetail getUserDetail()
        {
            return userDetail;
        }

        public static void setUserDetail(IUserDetail userDetail)
        {
            UserDetailResolver.userDetail = userDetail;
        }
    }
}
