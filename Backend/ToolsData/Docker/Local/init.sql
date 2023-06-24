CREATE DATABASE IF NOT EXISTS mq_main;
DROP USER IF EXISTS 'developer'@'%';
CREATE USER 'developer'@'%' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON mq_main.* TO 'developer'@'%';
FLUSH PRIVILEGES;
USE mq_main;