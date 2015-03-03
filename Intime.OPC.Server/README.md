#站点功能使用说明
## 用户权限相关

* 开关控制
    ```
  Web.config中的AppSettings中键AccessToken:Enabled 设置true开启，false关闭
    ```
* 用户Id的获取
  ```
  Action([UserId] int userId)
  ```

* 用户信息获取
   ```
  Action([UserProfile] UserProfile userProfile)
  ```

* 角色控制
 ```
  [Authorize(Roles = "管理员,店长")]
  Action(:::)
  ```

* 用户访问控制
    ```
   [Authorize(Users = "admin,dahongmen")]
   Action(:::)
   ```