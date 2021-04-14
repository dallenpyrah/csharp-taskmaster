USE jobsapi;

-- DROP TABLE profiles;
-- DROP TABLE boards;
-- DROP TABLE thetodos;
-- DROP TABLE todos;

-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );


-- CREATE TABLE boards 
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     title VARCHAR(255) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     open TINYINT(1),
--     creatorId VARCHAR(255),

--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE
-- );
-- CREATE TABLE thetodos
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     title VARCHAR(255) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     boardId INT NOT NULL,
--     creatorId VARCHAR(255),

--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE,

--     FOREIGN KEY (boardId)
--     REFERENCES boards (id)
--     ON DELETE CASCADE
-- );

CREATE TABLE boardmembers
(
    id INT NOT NULL AUTO_INCREMENT,
    memberId VARCHAR(255) NOT NULL,
    boardId INT NOT NULL,
    creatorId VARCHAR(255),

    PRIMARY KEY (id),

    FOREIGN KEY (creatorId)
    REFERENCES profiles (id)
    ON DELETE CASCADE,

    FOREIGN KEY (memberId)
    REFERENCES profiles (id)
    ON DELETE CASCADE,

    FOREIGN KEY (boardId)
    REFERENCES boards (id)
    ON DELETE CASCADE
);