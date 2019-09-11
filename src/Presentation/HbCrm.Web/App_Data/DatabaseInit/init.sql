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

DROP TABLE IF EXISTS `admin`;

CREATE TABLE `admin` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateBy` int(11) NOT NULL,
  `CreatebyName` varchar(50) DEFAULT NULL,
  `CreateDate` datetime(6) DEFAULT NULL,
  `LastUpdateBy` int(11) NOT NULL,
  `LastUpdateByName` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime(6) DEFAULT NULL,
  `Guid` varchar(64) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `Password` varchar(64) DEFAULT NULL,
  `NickName` varchar(50) DEFAULT NULL,
  `Email` varchar(200) DEFAULT NULL,
  `MobilePhone` varchar(20) DEFAULT NULL,
  `QQ` longtext,
  `WeChar` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `admin` */

LOCK TABLES `admin` WRITE;

insert  into `admin`(`Id`,`CreateBy`,`CreatebyName`,`CreateDate`,`LastUpdateBy`,`LastUpdateByName`,`LastUpdateDate`,`Guid`,`UserName`,`Password`,`NickName`,`Email`,`MobilePhone`,`QQ`,`WeChar`) values (1,0,NULL,NULL,0,NULL,NULL,'qwewfsdfged','lily','123456',NULL,NULL,NULL,NULL,NULL);

UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
