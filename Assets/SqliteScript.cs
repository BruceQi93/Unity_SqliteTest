/************************
 * Title:
 * Function：
 * 	－ 读取Sqlite数据库
 * Used By：	GameController
 * Author:	qwt
 * Date:	
 * Version:	1.0
 * Description:
 *
************************/

using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;//引入命名空间

public class SqliteScript : MonoBehaviour {

    //声明数据库连接对象，通过该对象与数据库文件所在路径进行连接，进而打开数据库
    SqliteConnection con;
    //数据库文件所在路径
    string path;
    //数据库命令
    SqliteCommand command;
    SqliteDataReader reader;
	
	void Start () {
        //要连接的数据库文件路径
        path = "Data Source="+ Application.streamingAssetsPath + "/part01.sqlite";
        //通过路径创建出连接对象
        con = new SqliteConnection(path);
        //打开数据库文件
        con.Open();
        //创建指令对象实例
        command = con.CreateCommand();
        ReadSqlite();
	}
	
	void ReadSqlite()
    {
        #region 第一种执行方式，用于增，删，改操作
        //数据库语句
        command.CommandText = "insert into hero values('张三',10,20,1)";
        //返回受影响的行数
        int count = command.ExecuteNonQuery();
        #endregion

        #region 第二种执行方式，用在查询结果只有一个的情况
        command.CommandText = "select ap,ad from hero where heroName='张三'";
        object obj = command.ExecuteScalar();
        #endregion

        #region 第三种执行方式，返回所有的查询结果
        command.CommandText = "select * from hero";
        reader = command.ExecuteReader();
        //如果读取了下一行，返回值为TRUE，否则为FALSE
        while (reader.Read())
        {
            //把每一行的毎一列读取出来
            for (int i = 0; i < reader.FieldCount; i++)
            {
                print(reader.GetValue(i).ToString() + " ");
            }
        }
        #endregion
    }
}
