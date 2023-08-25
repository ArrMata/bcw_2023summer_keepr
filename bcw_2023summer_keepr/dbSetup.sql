CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE keeps(
  id INT NOT NULL PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(100) NOT NULL,
  description VARCHAR(500),
  img VARCHAR(400) NOT NULL,
  views INT DEFAULT 0,
  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';

CREATE TABLE vaults(
  id INT NOT NULL PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(100) NOT NULL,
  description VARCHAR(500),
  img VARCHAR(400) NOT NULL,
  isPrivate BOOLEAN DEFAULT false,
  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';

CREATE TABLE vaultkeeps(
  id INT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id),
  FOREIGN KEY (vaultId) REFERENCES vaults(id),
  FOREIGN KEY (keepId) REFERENCES keeps(id)
) default charset utf8 COMMENT '';

DROP TABLE keeps;
DROP TABLE vaults;
DROP TABLE vaultkeeps;