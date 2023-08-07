
/* 2023-08-03 23:28:00 [62 ms] */ 
CREATE TABLE
    "Comment" (
        "id" INTEGER NOT NULL CONSTRAINT "PK_Comment" PRIMARY KEY AUTOINCREMENT,
        "userId" INTEGER NOT NULL,
        "postId" INTEGER NOT NULL,
        "content" TEXT NOT NULL,
        "createdAt" TEXT NOT NULL
    )


;
/* 2023-08-03 23:28:20 [68 ms] */ 
CREATE TABLE
    "Message" (
        "id" INTEGER NOT NULL CONSTRAINT "PK_Message" PRIMARY KEY AUTOINCREMENT,
        "senderEmail" TEXT NOT NULL,
        "receiverEmail" TEXT NOT NULL,
        "content" TEXT NOT NULL,
        "createdAt" TEXT NOT NULL
    )
;
/* 2023-08-03 23:28:36 [56 ms] */ 
CREATE TABLE
    "Post" (
        "id" INTEGER NOT NULL CONSTRAINT "PK_Post" PRIMARY KEY AUTOINCREMENT,
        "userId" INTEGER NOT NULL,
        "title" TEXT NOT NULL,
        "content" TEXT NOT NULL,
        "createdAt" TEXT NOT NULL,
        "tagId" INTEGER NOT NULL,
        "likes" INTEGER NOT NULL,
        "dislikes" INTEGER NOT NULL
    )
;
/* 2023-08-03 23:28:59 [70 ms] */ 
CREATE TABLE
    "Tag" (
        "id" INTEGER NOT NULL CONSTRAINT "PK_Tag" PRIMARY KEY AUTOINCREMENT,
        "name" TEXT NOT NULL
    )
;
/* 2023-08-03 23:29:30 [71 ms] */ 
CREATE TABLE
    "User" (
        "id" INTEGER NOT NULL CONSTRAINT "PK_User" PRIMARY KEY AUTOINCREMENT,
        "name" TEXT NOT NULL,
        "lastName" TEXT NOT NULL,
        "email" TEXT NOT NULL,
        "passwordHash" TEXT NOT NULL,
        "token" TEXT NULL,
        "role" TEXT NOT NULL
    )
;
/* 2023-08-03 23:29:56 [72 ms] */ 
CREATE TABLE
    "__EFMigrationsHistory" (
        "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
        "ProductVersion" TEXT NOT NULL
    )
;
/* 2023-08-06 00:12:13 [71 ms] */ 
INSERT INTO sqlite_sequence(name,seq) VALUES('User','1');
/* 2023-08-06 00:12:21 [67 ms] */ 
INSERT INTO sqlite_sequence(name,seq) VALUES('Tag','2');
/* 2023-08-06 00:12:27 [62 ms] */ 
INSERT INTO sqlite_sequence(name,seq) VALUES('Post','2');
INSERT INTO "Tag"(id,name) VALUES(1,'Tag1');
INSERT INTO "Tag"(id,name) VALUES(2,'Tag2');