DROP DATABASE IF EXISTS "TwitterCloneDB";
CREATE DATABASE "TwitterCloneDB";

\connect "TwitterCloneDB"

DROP TABLE IF EXISTS "posts";
DROP SEQUENCE IF EXISTS posts_post_id_seq;
CREATE SEQUENCE posts_post_id_seq INCREMENT 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1;

CREATE TABLE "public"."posts" (
    "post_id" integer DEFAULT nextval('posts_post_id_seq') NOT NULL,
    "post_user_id" integer NOT NULL,
    "post_body" text NOT NULL,
    CONSTRAINT "posts_pkey" PRIMARY KEY ("post_id")
) WITH (oids = false);

INSERT INTO "posts" ("post_user_id", "post_body") VALUES
(1, 'Innlegg nummer 1'),
(2, 'Innlegg nummer 2'),
(3, 'Innlegg nummer 3');

DROP TABLE IF EXISTS "users";
DROP SEQUENCE IF EXISTS users_user_id_seq;
CREATE SEQUENCE users_user_id_seq INCREMENT 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1;

CREATE TABLE "public"."users" (
    "user_id" integer DEFAULT nextval('users_user_id_seq') NOT NULL,
    "user_name" character varying(100) NOT NULL,
    "user_token" character varying(10) NOT NULL,
    CONSTRAINT "users_pkey" PRIMARY KEY ("user_id")
) WITH (oids = false);

INSERT INTO "users" ("user_name", "user_token") VALUES
('1',	'1111111111'),
('2',	'2222222222'),
('3',	'3333333333');

ALTER TABLE ONLY "public"."posts" ADD CONSTRAINT "posts_post_user_id_fkey" FOREIGN KEY (post_user_id) REFERENCES users(user_id) ON UPDATE CASCADE ON DELETE CASCADE NOT DEFERRABLE;

-- 2023-01-20 01:28:26.125927+00
