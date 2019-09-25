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

/*Table structure for table `admin` */

DROP TABLE IF EXISTS `sys_admin`;

CREATE TABLE `sys_admin` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateBy` int(11) NOT NULL,
  `CreatebyName` varchar(50) NOT NULL,
  `CreateDate` datetime(6) NOT NULL,
  `LastUpdateBy` int(11) NOT NULL,
  `LastUpdateByName` varchar(50) NOT NULL,
  `LastUpdateDate` datetime(6) NOT NULL,
  `Guid` varchar(64) NOT NULL,
  `UserName` varchar(50) NOT NULL,
  `Password` varchar(64) NOT NULL,
  `NickName` varchar(50) DEFAULT NULL,
  `Email` varchar(200) DEFAULT NULL,
  `MobilePhone` varchar(20) DEFAULT NULL,
  `QQ` varchar(50) DEFAULT NULL,
  `WeChar` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `sys_role`;

CREATE TABLE `sys_role` (
   `Id` int(11) NOT NULL AUTO_INCREMENT, 
  `CreateBy` int(11) NOT NULL,
  `CreatebyName` varchar(50) NOT NULL,
  `CreateDate` datetime(6) NOT NULL,
  `LastUpdateBy` int(11) NOT NULL,
  `LastUpdateByName` varchar(50) NOT NULL,
  `LastUpdateDate` datetime(6) NOT NULL,
  `RoleName` VARCHAR(30) NOT NULL,
  `RoleSystermName` VARCHAR(30) NOT NULL,
  `RoleRemark` VARCHAR(255) DEFAULT NULL,
  `RoleStatus` INT(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=INNODB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;


DROP TABLE IF EXISTS `sys_adminrole`;

CREATE TABLE `sys_adminrole` (
   `Id` int(11) NOT NULL AUTO_INCREMENT,  
  `CreateBy` int(11) NOT NULL,
  `CreatebyName` varchar(50) NOT NULL,
  `CreateDate` datetime(6) NOT NULL,
  `LastUpdateBy` int(11) NOT NULL,
  `LastUpdateByName` varchar(50) NOT NULL,
  `LastUpdateDate` datetime(6) NOT NULL,
  `AdminId` INT(11) NOT NULL, 
  `RoleId` INT(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=INNODB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;


DROP TABLE IF EXISTS `sys_function`;

CREATE TABLE `sys_function` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `CreateBy` INT(11) NOT NULL,
  `CreatebyName` VARCHAR(50) NOT NULL,
  `CreateDate` DATETIME(6) NOT NULL,
  `LastUpdateBy` INT(11) NOT NULL,
  `LastUpdateByName` VARCHAR(50) NOT NULL,
  `LastUpdateDate` DATETIME(6) NOT NULL,
  `FunctionName` VARCHAR(50) NOT NULL,
  `FunctionSystermName` VARCHAR(50) NOT NULL,
  `FunctionSort` INT(11) NOT NULL,
  `MenuId` INT(11) NOT NULL,
  `FunctionRemark` VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY(`Id`)
) ENGINE=INNODB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8;

DROP TABLE IF EXISTS `sys_functionrole`;

CREATE TABLE `sys_functionrole` (
   `Id` INT(11) NOT NULL AUTO_INCREMENT,  
  `CreateBy` INT(11) NOT NULL,
  `CreatebyName` VARCHAR(50) NOT NULL,
  `CreateDate` DATETIME(6) NOT NULL,
  `LastUpdateBy` INT(11) NOT NULL,
  `LastUpdateByName` VARCHAR(50) NOT NULL,
  `LastUpdateDate` DATETIME(6) NOT NULL,
  `FunctionId` INT(11) NOT NULL, 
  `RoleId` INT(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=INNODB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;


DROP TABLE IF EXISTS `sys_menu`;

CREATE TABLE `sys_menu` (
 `Id` int(11) NOT NULL AUTO_INCREMENT, 
  `CreateBy` int(11) NOT NULL,
  `CreatebyName` varchar(50) NOT NULL,
  `CreateDate` datetime(6) NOT NULL,
  `LastUpdateBy` int(11) NOT NULL,
  `LastUpdateByName` varchar(50) NOT NULL,
  `LastUpdateDate` datetime(6) NOT NULL,
  `MenuName` VARCHAR(30) NOT NULL,
  `MenuSystermName` VARCHAR(30) NOT NULL,
  `MenuRemark` VARCHAR(200) DEFAULT NULL,
  `MenuUrl` VARCHAR(255) NOT NULL,
  `ParentMenuId` INT(11) DEFAULT 0 NOT NULL,
  `MenuIcon` VARCHAR(50) DEFAULT NULL,
  `MenuSort` INT(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=INNODB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `sys_menurole`;

CREATE TABLE `sys_menurole` (
   `Id` INT(11) NOT NULL AUTO_INCREMENT,  
  `CreateBy` INT(11) NOT NULL,
  `CreatebyName` VARCHAR(50) NOT NULL,
  `CreateDate` DATETIME(6) NOT NULL,
  `LastUpdateBy` INT(11) NOT NULL,
  `LastUpdateByName` VARCHAR(50) NOT NULL,
  `LastUpdateDate` DATETIME(6) NOT NULL,
  `MenuId` INT(11) NOT NULL, 
  `RoleId` INT(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=INNODB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;



/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
