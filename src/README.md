一个基于 ASP.NET Boilerplate 开发框架的示例项目
-------------------------------------------------------------------------

前端：
思想：响应式布局、单页面应用、图标字体、MVVM、JS模块化、JS模板引擎

技术/框架：HTML5、CSS3、LESS、Jquery、Bootstrap、AngularJs

组件： Webuploader、Ueditor/Umeditor、Highcharts、Jquery.dataTables、Jquery.form、Jquery.validate、Jquery.Jcrop、Jquery.mCustomScrollbar、Spectrum、Toastr、BlockUI、SuperSlide，还有一大堆小的Jquery插件就省略了

 

后端：

思想： DDD（领域驱动设计）、TDD（测试驱动设计）、DI/AOP（依赖注入/面向切面编程）、模块化开发、异步编程、分布式架构、敏捷开发之SCRUM

技术/框架：Asp.net MVC5、C# 5.0、Entity Framework 6、xUnit+NSubstitute+Shouldly、aspnetboilerplate

工具：Git、VS2013 、MySQL、Sql Server、MongoDB、Redis

开源组件：AspNet.Identity、AutoMapper、Castle.Windsor、MiniProfiler


这个项目以模块化方式构建应用服务层。示例项目详细记录在这些Codeproject上的文章:

* Angular&EF version: http://www.codeproject.com/Articles/791740/Using-AngularJs-ASP-NET-MVC-Web-API-and-EntityFram
* Unit Testing for this project: http://www.codeproject.com/Articles/871786/Unit-testing-in-Csharp-using-xUnit-Entity-Framewor

示例项目

在示例项目文件夹在 VS2013 中打开项目. 运行:

检查 web.config 个的数据库连接字符串，根据自己的需要修改.
运行数据库迁移 (运行在程序包管理控制台中，默认项目选择你要进行迁移的模块项目'/Modules/*',执行 'Update-Database' 命令进行数据迁移) 进行数据库创建和数据初始化.
F5，运行应用程序! 
