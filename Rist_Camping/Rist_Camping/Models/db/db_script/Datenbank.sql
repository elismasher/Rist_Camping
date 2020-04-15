create database if not exists db_Rist_Camping collate utf8mb4_general_ci;
use db_Rist_Camping;

create table reservations(
	id int not null auto_increment,
    firstname varchar(100),
    lastname varchar(100) not null,
    email varchar(100) not null,
	arrivalDate DATETIME not null,
	departureDate DATETIME not null,
    numberOfAdults INT not null,
    numberOfChildren INT not null,
    typeOfPlace INT not null,
    
    constraint id_PK primary key(id)
)engine=InnoDB;

create table users(
	id int not null auto_increment,
    firstname varchar(100) not null,
    lastname varchar(100) not null,
    email varchar(100) not null,
    gender int not null,
    userRole int not null,
    username varchar(100) not null unique,
    password varchar(128) not null,
    
    constraint id_PK primary key(id)
)engine=InnoDB;


-- UPDATE users SET password = sha2("neues Passwort", 256) WHERE id = 1;

INSERT INTO reservations VALUES(null, "Elias", "Rist", "test@test.at", now(), now(), 1, 0, 3);
INSERT INTO users VALUES(null, "Elias", "Rist", "elias.rist@nms2.at", 0, 1, "Elismasher", sha2("Klexi2408", 256));

select * from reservations;

