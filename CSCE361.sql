/* 	Host: cse.unl.edu
	Username: yalee
	Password:  h5@]Wt
*/
USE yalee;

DROP TABLE IF EXISTS Comment;
DROP TABLE IF EXISTS Photo;
DROP TABLE IF EXISTS User;

CREATE TABLE User (
	UserID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Username VARCHAR(255) NOT NULL,
	FirstName VARCHAR(255) NOT NULL,
	LastName VARCHAR(255) NOT NULL,
	ProfilePictureFileLoc VARCHAR(255),
	Age INT(3), /* Date of birth instead?*/  
	CONSTRAINT UniqueKeyUser UNIQUE INDEX(Username)
) Engine=InnoDB,COLLATE=latin1_general_cs;

CREATE TABLE Photo (
	PhotoID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	ImageFileLoc VARCHAR(255), /* This should be unique */
	Longitude DOUBLE,
	Latitude DOUBLE,
	Caption VARCHAR(255),
	UserID INT NOT NULL,
	FOREIGN KEY (UserID) REFERENCES User(UserID),
	CONSTRAINT UniquePhoto UNIQUE INDEX(ImageFileLoc)
) Engine=InnoDB,COLLATE=latin1_general_cs;

CREATE TABLE Comment(
	CommentID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Content VARCHAR(255),
	CommentDate INT,
	PhotoID INT NOT NULL,
	UserID INT NOT NULL,
	FOREIGN KEY(PhotoID) REFERENCES Photo(PhotoID),
	FOREIGN KEY(UserID) REFERENCES User(UserID)
)Engine=InnoDB,COLLATE=latin1_general_cs;
