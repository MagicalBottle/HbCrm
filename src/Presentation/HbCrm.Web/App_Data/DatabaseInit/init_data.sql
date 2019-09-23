/*
SQLyog Ultimate v12.09 (64 bit)
MySQL - 5.6.17 : Database - hbcrm
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`hbcrm` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `hbcrm`;

/*Data for the table `sys_admin` */

LOCK TABLES `sys_admin` WRITE;

INSERT  INTO `sys_admin`(`Id`,`CreateBy`,`CreatebyName`,`CreateDate`,`LastUpdateBy`,`LastUpdateByName`,`LastUpdateDate`,`Guid`,`UserName`,`Password`,`NickName`,`Email`,`MobilePhone`,`QQ`,`WeChar`) VALUES (1,0,'init','2019-09-23 11:02:15.000000',0,'init','2019-09-23 11:02:15.000000','6fa8a7b3-782a-4ca4-b6ca-a49494746de9','Admin','123456',NULL,NULL,NULL,NULL,NULL),(2,1,'Admin','2019-09-23 11:05:37.000000',1,'Admin','2019-09-23 11:05:37.000000','cabfda64-acb6-4899-b802-59b0aee72ff5','lily','123456',NULL,NULL,NULL,NULL,NULL);

UNLOCK TABLES;

/*Data for the table `sys_adminrole` */

LOCK TABLES `sys_adminrole` WRITE;

INSERT  INTO `sys_adminrole`(`Id`,`CreateBy`,`CreatebyName`,`CreateDate`,`LastUpdateBy`,`LastUpdateByName`,`LastUpdateDate`,`AdminId`,`RoleId`) VALUES (1,1,'Admin','2019-09-23 11:07:39.000000',1,'Admin','2019-09-23 11:07:39.000000',1,1),(2,1,'Admin','2019-09-23 11:07:39.000000',1,'Admin','2019-09-23 11:07:39.000000',2,1);

UNLOCK TABLES;

/*Data for the table `sys_menu` */

LOCK TABLES `sys_menu` WRITE;

UNLOCK TABLES;

/*Data for the table `sys_role` */

LOCK TABLES `sys_role` WRITE;

INSERT  INTO `sys_role`(`Id`,`CreateBy`,`CreatebyName`,`CreateDate`,`LastUpdateBy`,`LastUpdateByName`,`LastUpdateDate`,`RoleName`,`RoleSystermName`,`RoleRemark`,`RoleStatus`) VALUES (1,1,'Admin','2019-09-23 11:06:27.000000',1,'Admin','2019-09-23 11:06:27.000000','超级管理员','super',NULL,1);

UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
