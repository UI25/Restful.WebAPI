USE [master]
GO 

IF DB_ID('WebAPI') IS NOT NULL
    set noexec on

CREATE DATABASE [WebAPI];
GO