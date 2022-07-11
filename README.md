# Tj-NetShop
数据库课设后端
* 07-01：初步配置数据库环境，搭建基本的Api接口和身份验证

* api接口详细调用:http://124.222.1.19:5000/Swagger/index.html
### Linux服务器中访问Oracle流程
首先登陆服务器，切换到终端，命令如下
```bash
docker exec -it oracle11g bash
su     #执行切换用户命令
helowin   #输入密码
su - oracle    #切换到oracle用户
sqlplus /nolog    #登陆sql
conn /as sysdba   #登陆根用户
conn shop/jy2051914  #切换到shop用户
```
> 后续执行sql命令即可
