using HbCrm.Core.Domain.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Xunit;

namespace HbCrm.Services.Tests
{

    public class CommonTest
    {
        [Fact]
        public void String_IndexOf()
        {


            var data = new List<SysMenu>()
                {
                    new SysMenu() { Id = 1, MenuName = "A1", MenuSort = 1, CreateDate = new DateTime(2018,01,01) },
                    new SysMenu() { Id = 2, MenuName = "B2", MenuSort = 2, CreateDate = new DateTime(2018,02,01) },
                    new SysMenu() { Id = 3, MenuName = "C3", MenuSort = 3, CreateDate = new DateTime(2018,03,01) },
                    new SysMenu() { Id = 4, MenuName = "D4", MenuSort = 4, CreateDate = new DateTime(2018,04,01) },
                };

            var query = data
                    .AsQueryable()
                    .OrderBy("MenuSort descending");
            var result = query.ToDynamicList<SysMenu>();


            string s1 = "1";
            string s2 = "12";
            int i = s2.IndexOf(s1);

            s1 = " ";
            s2 = " 222";
            i = s2.IndexOf(s1);

            s1 = " ";
            s2 = "222 ";
            i = s2.IndexOf(s1);

            s1 = " ";
            s2 = "2 22";
            i = s2.IndexOf(s1);


            s1 = "1";
            s2 = "222";
            i = s2.IndexOf(s1);

            s1 = "1";
            s2 = "";
            i = s2.IndexOf(s1);


            s1 = "1";
            s2 = " ";
            i = s2.IndexOf(s1);

            s1 = " ";
            s2 = " ";
            i = s2.IndexOf(s1);

            Assert.True(i > 0);
        }
    }
}
