CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Maintenances" (
    "Id" varchar(150) NOT NULL,
    "Description" varchar(250) NOT NULL,
    "UserId" uuid NOT NULL,
    "StatusMaintenance" int NOT NULL,
    "CreateDate" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '2021-09-22 19:18:17.86858',
    "Status" int NOT NULL,
    CONSTRAINT "PK_Maintenances" PRIMARY KEY ("Id")
);

CREATE TABLE "Stages" (
    "Id" varchar(150) NOT NULL,
    "MaintenanceId" varchar(150) NOT NULL,
    "Description" varchar(250) NOT NULL,
    "StatusStage" int NOT NULL,
    "Type" int NOT NULL,
    "Value" varchar(10000) NOT NULL,
    "CreateDate" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '2021-09-22 19:18:17.878403',
    "Status" int NOT NULL,
    CONSTRAINT "PK_Stages" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Stages_Maintenances_MaintenanceId" FOREIGN KEY ("MaintenanceId") REFERENCES "Maintenances" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_Stages_MaintenanceId" ON "Stages" ("MaintenanceId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210922221818_Initial', '5.0.9');

COMMIT;

START TRANSACTION;

ALTER TABLE "Stages" ALTER COLUMN "Value" TYPE varchar(100000);

ALTER TABLE "Stages" ALTER COLUMN "CreateDate" SET DEFAULT TIMESTAMP '2021-09-23 12:37:17.858785';

ALTER TABLE "Maintenances" ALTER COLUMN "CreateDate" SET DEFAULT TIMESTAMP '2021-09-23 12:37:17.849415';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210923153718_AlterValue', '5.0.9');

COMMIT;

