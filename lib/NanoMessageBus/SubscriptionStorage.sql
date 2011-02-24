CREATE TABLE Subscriptions
(
    Subscriber varchar(255) NOT NULL CHECK ( LEN(Subscriber) > 0 ),
    MessageType varchar(255) NOT NULL CHECK ( LEN(MessageType) > 0 ),
    Expiration smalldatetime NULL,
    CONSTRAINT PK_Subscribers PRIMARY KEY CLUSTERED ( Subscriber, MessageType )
);
CREATE UNIQUE NONCLUSTERED INDEX IX_Subscriptions ON Subscriptions
(
    MessageType,
    Expiration,
    Subscriber
);