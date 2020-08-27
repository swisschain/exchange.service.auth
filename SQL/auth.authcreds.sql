CREATE TABLE auth.authcreds
(
    "Id" character varying(255),
    "Email" character varying(1024),
    "Hash" character varying(10240),
    "Salt" character varying(1024),
    CONSTRAINT "authcreds-PK" PRIMARY KEY ("Id"),
    CONSTRAINT "authcreds-email" UNIQUE ("Email")

)
WITH (
    OIDS = FALSE
);
