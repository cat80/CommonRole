/*
SQLyog v10.2 
MySQL - 5.6.25 : Database - commonrole
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`commonrole` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `commonrole`;

/*Table structure for table `mp_role_action` */

DROP TABLE IF EXISTS `mp_role_action`;

CREATE TABLE `mp_role_action` (
  `ActionID` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键ID',
  `ResourceID` int(11) NOT NULL DEFAULT '0' COMMENT 'action对应控制器ID',
  `ControllerName` varchar(100) CHARACTER SET utf8 NOT NULL COMMENT '控制器名称',
  `ActionName` varchar(100) CHARACTER SET utf8 NOT NULL COMMENT 'action请求权限字段',
  `DisplayName` varchar(255) CHARACTER SET utf8 NOT NULL COMMENT '显示名称',
  `Sort` int(11) NOT NULL COMMENT '在菜单的位置',
  `Status` tinyint(1) NOT NULL DEFAULT '0' COMMENT '状态0，启用,1禁用',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `IsShowInMenu` tinyint(1) NOT NULL DEFAULT '0' COMMENT '是否在菜单中显示',
  `LastModifyTime` datetime NOT NULL COMMENT '最后修改时间',
  `Remarks` varchar(1000) CHARACTER SET utf8 NOT NULL COMMENT '备注',
  PRIMARY KEY (`ActionID`),
  UNIQUE KEY `Controll_Action_UK` (`ControllerName`,`ActionName`),
  KEY `FK_Reference_1` (`ResourceID`)
) ENGINE=MyISAM AUTO_INCREMENT=121 DEFAULT CHARSET=latin1 COMMENT='页面资源功能表';

/*Data for the table `mp_role_action` */

insert  into `mp_role_action`(`ActionID`,`ResourceID`,`ControllerName`,`ActionName`,`DisplayName`,`Sort`,`Status`,`CreateTime`,`IsShowInMenu`,`LastModifyTime`,`Remarks`) values (1,35,'controlpanel','changepassword','修改密码',0,1,'2013-11-06 16:59:26',0,'2013-11-08 10:37:08',''),(27,16,'role','index','资源管理',0,1,'2013-11-06 16:59:26',0,'2013-11-07 09:49:33',''),(28,16,'role','resources','资源列表',0,1,'2013-11-06 16:59:26',1,'2013-11-07 09:47:59',''),(29,16,'role','saveresource','保存资源',0,1,'2013-11-06 16:59:26',0,'2013-11-07 09:49:44',''),(30,16,'role','deleteresources','删除资源',0,1,'2013-11-06 16:59:27',0,'2013-11-07 09:49:31',''),(31,32,'role','groups','角色列表',0,1,'2013-11-06 16:59:27',0,'2013-11-07 09:48:36',''),(32,32,'role','savegroups','保存角色信息',0,1,'2013-11-06 16:59:27',0,'2013-11-07 09:49:33',''),(33,32,'role','deletegroups','删除角色',0,1,'2013-11-06 16:59:27',1,'2013-11-07 09:47:58',''),(34,33,'role','actions','功能列表',0,1,'2013-11-06 16:59:27',0,'2013-11-07 09:49:52',''),(35,33,'role','editaction','编辑功能',0,1,'2013-11-06 16:59:27',0,'2013-11-07 09:49:30',''),(36,33,'role','saveaction','保存功能',0,1,'2013-11-06 16:59:27',0,'2013-11-07 09:49:30',''),(37,33,'role','deleteaction','删除功能',0,1,'2013-11-06 16:59:27',0,'2013-11-07 09:49:32',''),(38,31,'role','groupactions','角色权限列表',0,1,'2013-11-06 16:59:27',0,'2013-11-07 09:48:25',''),(39,31,'role','savegroupactions','保存角色权限',0,1,'2013-11-06 16:59:27',0,'2013-11-07 09:49:52',''),(77,43,'controlpanel','myinfo','基本资料',1000,1,'0000-00-00 00:00:00',1,'2013-12-03 09:10:26',''),(78,43,'controlpanel','myaccounts','账户信息',900,1,'0000-00-00 00:00:00',1,'2013-12-03 09:11:33',''),(119,46,'controlpanel','saveadmin','保存用户信息',600,1,'0001-01-01 00:00:00',1,'2015-07-28 10:56:26',''),(118,46,'controlpanel','deleteadmin','删除用户',700,1,'0001-01-01 00:00:00',1,'2015-07-28 10:58:14',''),(117,46,'controlpanel','editadmin','编辑用户',900,1,'0001-01-01 00:00:00',1,'2015-07-28 10:54:19',''),(116,46,'controlpanel','adminlist','用户列表',1000,1,'0001-01-01 00:00:00',1,'2015-07-28 10:53:11','');

/*Table structure for table `mp_role_group` */

DROP TABLE IF EXISTS `mp_role_group`;

CREATE TABLE `mp_role_group` (
  `GroupID` int(11) NOT NULL AUTO_INCREMENT COMMENT '分组ID',
  `GroupName` varchar(125) CHARACTER SET utf8 NOT NULL COMMENT '分组名称',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `Auth_Action` text CHARACTER SET utf8 NOT NULL COMMENT '分组的action权限列表(,分隔)',
  `Auth_Resource` text CHARACTER SET utf8 NOT NULL COMMENT '用户可访问资源列表(,分隔)',
  `State` int(11) NOT NULL DEFAULT '0' COMMENT '角色状态',
  PRIMARY KEY (`GroupID`)
) ENGINE=MyISAM AUTO_INCREMENT=36 DEFAULT CHARSET=latin1;

/*Data for the table `mp_role_group` */

insert  into `mp_role_group`(`GroupID`,`GroupName`,`CreateTime`,`Auth_Action`,`Auth_Resource`,`State`) values (2,'系统管理员','0001-01-01 00:00:00','116,117,118,119,27,28,29,30,34,35,36,37,31,32,33,38,39,77,78,1','17,46,47,15,16,33,32,31,34,43,35',1);

/*Table structure for table `mp_role_resource` */

DROP TABLE IF EXISTS `mp_role_resource`;

CREATE TABLE `mp_role_resource` (
  `ResourceID` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键ID',
  `ResourceName` varchar(255) CHARACTER SET utf8 NOT NULL COMMENT '资源名称',
  `ParentID` int(11) NOT NULL COMMENT '父级ID',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序号',
  `ShowInMenu` int(11) NOT NULL COMMENT '是否在菜单中显示',
  `State` int(11) NOT NULL COMMENT '是否启用',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `Url` varchar(500) CHARACTER SET utf8 NOT NULL COMMENT '链接地址',
  PRIMARY KEY (`ResourceID`)
) ENGINE=MyISAM AUTO_INCREMENT=66 DEFAULT CHARSET=latin1 COMMENT='页面资源表';

/*Data for the table `mp_role_resource` */

insert  into `mp_role_resource`(`ResourceID`,`ResourceName`,`ParentID`,`Sort`,`ShowInMenu`,`State`,`CreateTime`,`Url`) values (17,'账户管理',0,1100,1,1,'0001-01-01 00:00:00',''),(15,'权限管理',0,900,1,1,'0001-01-01 00:00:00',',123'),(16,'资源管理',15,1000,1,1,'0001-01-01 00:00:00','/role/resources'),(43,'账户信息',34,1100,1,1,'0001-01-01 00:00:00','/controlpanel/myinfo'),(31,'角色权限管理',15,700,1,1,'0001-01-01 00:00:00','/role/groupactions'),(32,'角色管理',15,800,1,1,'0001-01-01 00:00:00','/role/groups'),(33,'功能管理',15,900,1,1,'0001-01-01 00:00:00','/role/actions'),(34,'个人资料管理',0,800,1,1,'0001-01-01 00:00:00',''),(35,'修改密码',34,1000,1,1,'0001-01-01 00:00:00','/controlpanel/changepassword'),(46,'用户列表',17,880,1,1,'0001-01-01 00:00:00','/controlpanel/adminlist'),(47,'添加用户',17,850,1,1,'0001-01-01 00:00:00','/controlpanel/editadmin'),(65,'adfs3',0,0,1,1,'0001-01-01 00:00:00','');

/*Table structure for table `mp_user_info` */

DROP TABLE IF EXISTS `mp_user_info`;

CREATE TABLE `mp_user_info` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT COMMENT '用户编号',
  `DeptID` int(11) NOT NULL DEFAULT '0' COMMENT '部门ID',
  `UserLoginName` varchar(50) NOT NULL COMMENT '用户名登陆名',
  `UserPassword` varchar(50) NOT NULL COMMENT '密码',
  `UserName` varchar(30) NOT NULL COMMENT '用户名',
  `IsAdmin` varchar(3) NOT NULL DEFAULT '0' COMMENT '是否为员',
  `Description` varchar(1000) NOT NULL COMMENT '描述',
  `CreateTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '登陆时间',
  `LastModifyTime` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' COMMENT '最后登陆时间',
  `ModifiedUserID` varchar(36) NOT NULL COMMENT '修改人ID',
  `MobliePhone` varchar(50) NOT NULL COMMENT '联系电话',
  `WorkPhone` varchar(50) NOT NULL COMMENT '工作电话',
  `Email` varchar(100) NOT NULL COMMENT '电子邮箱',
  `Status` int(11) NOT NULL COMMENT '状态.0。禁用。1.激活',
  `Address` varchar(150) NOT NULL COMMENT '联系地址',
  `Deleted` int(1) NOT NULL DEFAULT '0' COMMENT '是否已经删除',
  `Groups` varchar(1000) NOT NULL COMMENT '所属于分组',
  `Level` int(11) NOT NULL COMMENT '管理员等级（0,初级管理员，1,高级管理)',
  `MaxCreateNumber` int(11) NOT NULL DEFAULT '0' COMMENT '最大创建数量',
  `ExpireTime` datetime NOT NULL COMMENT '到期日期',
  `Province` varchar(50) NOT NULL COMMENT '所属于省份',
  `City` varchar(50) NOT NULL COMMENT '所属于城市',
  `GroupNames` varchar(1000) NOT NULL DEFAULT '' COMMENT '所属分组名',
  `MakeRate` decimal(6,2) NOT NULL COMMENT '创建商家包制作收费比例',
  `NotMakeRate` decimal(6,2) NOT NULL COMMENT '创建商家不制作收费比较',
  `AgentRate` decimal(6,2) NOT NULL COMMENT '发展代理的转账折扣率',
  `CreateUser` int(11) NOT NULL COMMENT '创建者用户',
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `user_user_name_idx` (`UserLoginName`),
  KEY `user_user_password_idx` (`UserPassword`),
  KEY `FK_Reference_3` (`DeptID`)
) ENGINE=InnoDB AUTO_INCREMENT=98 DEFAULT CHARSET=utf8 COMMENT='管理用户';

/*Data for the table `mp_user_info` */

insert  into `mp_user_info`(`UserID`,`DeptID`,`UserLoginName`,`UserPassword`,`UserName`,`IsAdmin`,`Description`,`CreateTime`,`LastModifyTime`,`ModifiedUserID`,`MobliePhone`,`WorkPhone`,`Email`,`Status`,`Address`,`Deleted`,`Groups`,`Level`,`MaxCreateNumber`,`ExpireTime`,`Province`,`City`,`GroupNames`,`MakeRate`,`NotMakeRate`,`AgentRate`,`CreateUser`) values (1,2,'cat80','56d9b81c3697c1287cda8e553dea2556','苏志平','','','2013-06-25 16:32:13','2015-07-29 15:04:53','','112','222','456@qq.com',1,'2222',0,'2',1,10000,'2099-10-07 00:00:00','','','系统管理员','30.00','30.00','30.00',0);

/*Table structure for table `mp_user_login_his` */

DROP TABLE IF EXISTS `mp_user_login_his`;

CREATE TABLE `mp_user_login_his` (
  `HisID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL COMMENT '用户名',
  `UserType` int(11) NOT NULL COMMENT '用户类型.0,普通商家。1，管理员',
  `UserName` varchar(255) NOT NULL COMMENT '用户名',
  `LoginTime` datetime NOT NULL COMMENT '登陆时间',
  `LoginIP` varchar(255) NOT NULL COMMENT '登陆IP',
  PRIMARY KEY (`HisID`)
) ENGINE=InnoDB AUTO_INCREMENT=2254 DEFAULT CHARSET=utf8 COMMENT='用户登陆历史表';

/*Data for the table `mp_user_login_his` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
