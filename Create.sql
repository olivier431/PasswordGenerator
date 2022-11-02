CREATE DATABASE PasswordGenerator;

USE PasswordGenerator;

create table users
(
    id       integer primary key auto_increment,
    username varchar(20) not null unique check ( length(username) >= 1 ),
    fullname text,
    password text   not null check ( length(password) >= 8 ),
    email    text,
    last_connection datetime default '0-0-0'
);


create table passwords
(
    id       integer primary key auto_increment,
    user_id  int          not null references users (id),
    site     varchar(255) not null,
    login    varchar(255),
    password text  not null,
    unique (user_id, site, login),
    CreatedAt datetime default NOW(),
    ModifiedAt datetime default '0-0-0'

);


create table user_data
(
    id      integer primary key auto_increment,
    user_id int  not null references users (id),
    keyStr  text not null,
    data    text not null
);
