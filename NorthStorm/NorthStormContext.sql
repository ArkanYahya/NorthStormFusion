IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Genders] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Genders] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [GovernmentalInstituteClassification] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_GovernmentalInstituteClassification] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Grades] (
        [Id] int NOT NULL IDENTITY,
        [GradeNumber] int NOT NULL,
        [GradeAsWriting] nvarchar(13) NOT NULL,
        [Stage01] int NOT NULL,
        [Stage02] int NOT NULL,
        [Stage03] int NOT NULL,
        [Stage04] int NOT NULL,
        [Stage05] int NOT NULL,
        [Stage06] int NOT NULL,
        [Stage07] int NOT NULL,
        [Stage08] int NOT NULL,
        [Stage09] int NOT NULL,
        [Stage10] int NOT NULL,
        [Stage11] int NOT NULL,
        [AnnualBonus] int NOT NULL,
        [MinimumDuration] int NOT NULL,
        CONSTRAINT [PK_Grades] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [JobTitleClassifications] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_JobTitleClassifications] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [LocationClassifications] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_LocationClassifications] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Nationalities] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Nationalities] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Races] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Races] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Recruitments] (
        [Id] int NOT NULL IDENTITY,
        [ReferenceNo] nvarchar(max) NOT NULL,
        [ReferenceDate] datetime2 NOT NULL,
        [Subject] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Recruitments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Religiones] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Religiones] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [State] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_State] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Statuses] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Statuses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Locations] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [ParentLocationId] int NULL,
        [LocationClassificationId] int NULL,
        CONSTRAINT [PK_Locations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Locations_LocationClassifications_LocationClassificationId] FOREIGN KEY ([LocationClassificationId]) REFERENCES [LocationClassifications] ([Id]),
        CONSTRAINT [FK_Locations_Locations_ParentLocationId] FOREIGN KEY ([ParentLocationId]) REFERENCES [Locations] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [GovernmentalInstitutes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [ClassificationId] int NULL,
        [ParentGovernmentalInstituteId] int NULL,
        [LocationId] int NULL,
        CONSTRAINT [PK_GovernmentalInstitutes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GovernmentalInstitutes_GovernmentalInstituteClassification_ClassificationId] FOREIGN KEY ([ClassificationId]) REFERENCES [GovernmentalInstituteClassification] ([Id]),
        CONSTRAINT [FK_GovernmentalInstitutes_GovernmentalInstitutes_ParentGovernmentalInstituteId] FOREIGN KEY ([ParentGovernmentalInstituteId]) REFERENCES [GovernmentalInstitutes] ([Id]),
        CONSTRAINT [FK_GovernmentalInstitutes_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Certificates] (
        [Id] int NOT NULL IDENTITY,
        [Degree] nvarchar(max) NOT NULL,
        [GlobalSpecialization] nvarchar(max) NOT NULL,
        [AccurateSpecialization] nvarchar(max) NOT NULL,
        [Year] int NOT NULL,
        [UniversityId] int NOT NULL,
        CONSTRAINT [PK_Certificates] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Certificates_GovernmentalInstitutes_UniversityId] FOREIGN KEY ([UniversityId]) REFERENCES [GovernmentalInstitutes] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Employees] (
        [Id] int NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [MiddleName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [FourthName] nvarchar(max) NULL,
        [SurName] nvarchar(max) NULL,
        [MotherFirstName] nvarchar(max) NULL,
        [MotherMiddleName] nvarchar(max) NULL,
        [MotherLastName] nvarchar(max) NULL,
        [BirthDate] datetime2 NOT NULL,
        [CivilNumber] nvarchar(max) NULL,
        [IBAN] nvarchar(max) NULL,
        [GenderId] int NULL,
        [ReligionId] int NULL,
        [RaceId] int NULL,
        [NationalityId] int NULL,
        [StatusId] int NULL,
        [RecruitmentId] int NULL,
        [CertificateId] int NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Employees_Certificates_CertificateId] FOREIGN KEY ([CertificateId]) REFERENCES [Certificates] ([Id]),
        CONSTRAINT [FK_Employees_Genders_GenderId] FOREIGN KEY ([GenderId]) REFERENCES [Genders] ([Id]),
        CONSTRAINT [FK_Employees_Nationalities_NationalityId] FOREIGN KEY ([NationalityId]) REFERENCES [Nationalities] ([Id]),
        CONSTRAINT [FK_Employees_Races_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Races] ([Id]),
        CONSTRAINT [FK_Employees_Recruitments_RecruitmentId] FOREIGN KEY ([RecruitmentId]) REFERENCES [Recruitments] ([Id]),
        CONSTRAINT [FK_Employees_Religiones_ReligionId] FOREIGN KEY ([ReligionId]) REFERENCES [Religiones] ([Id]),
        CONSTRAINT [FK_Employees_Statuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Statuses] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Levels] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [ParentLevelId] int NULL,
        [LocationId] int NULL,
        [EmployeeId] int NULL,
        CONSTRAINT [PK_Levels] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Levels_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]),
        CONSTRAINT [FK_Levels_Levels_ParentLevelId] FOREIGN KEY ([ParentLevelId]) REFERENCES [Levels] ([Id]),
        CONSTRAINT [FK_Levels_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [Salary] (
        [Id] int NOT NULL IDENTITY,
        [BasicSalary] int NOT NULL,
        [EmployeeId] int NULL,
        CONSTRAINT [PK_Salary] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Salary_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [JobTitles] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [ParentJobTitleId] int NULL,
        [ClassificationId] int NULL,
        [GradeId] int NULL,
        [EmployeeId] int NULL,
        [GovernmentalInstituteClassificationId] int NULL,
        [JobTitleClassificationId] int NULL,
        [JobTitleId] int NULL,
        [LevelId] int NULL,
        CONSTRAINT [PK_JobTitles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_JobTitles_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]),
        CONSTRAINT [FK_JobTitles_GovernmentalInstituteClassification_GovernmentalInstituteClassificationId] FOREIGN KEY ([GovernmentalInstituteClassificationId]) REFERENCES [GovernmentalInstituteClassification] ([Id]),
        CONSTRAINT [FK_JobTitles_Grades_GradeId] FOREIGN KEY ([GradeId]) REFERENCES [Grades] ([Id]),
        CONSTRAINT [FK_JobTitles_JobTitleClassifications_ClassificationId] FOREIGN KEY ([ClassificationId]) REFERENCES [JobTitleClassifications] ([Id]),
        CONSTRAINT [FK_JobTitles_JobTitleClassifications_JobTitleClassificationId] FOREIGN KEY ([JobTitleClassificationId]) REFERENCES [JobTitleClassifications] ([Id]),
        CONSTRAINT [FK_JobTitles_JobTitles_JobTitleId] FOREIGN KEY ([JobTitleId]) REFERENCES [JobTitles] ([Id]),
        CONSTRAINT [FK_JobTitles_JobTitles_ParentJobTitleId] FOREIGN KEY ([ParentJobTitleId]) REFERENCES [JobTitles] ([Id]),
        CONSTRAINT [FK_JobTitles_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Levels] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [JobTransfers] (
        [Id] int NOT NULL IDENTITY,
        [ReferenceNo] nvarchar(max) NOT NULL,
        [ReferenceDate] datetime2 NOT NULL,
        [Subject] nvarchar(max) NOT NULL,
        [DestinationLevelId] int NOT NULL,
        CONSTRAINT [PK_JobTransfers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_JobTransfers_Levels_DestinationLevelId] FOREIGN KEY ([DestinationLevelId]) REFERENCES [Levels] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE TABLE [EmployeeJobTransfer] (
        [EmployeesId] int NOT NULL,
        [JobTransfersId] int NOT NULL,
        CONSTRAINT [PK_EmployeeJobTransfer] PRIMARY KEY ([EmployeesId], [JobTransfersId]),
        CONSTRAINT [FK_EmployeeJobTransfer_Employees_EmployeesId] FOREIGN KEY ([EmployeesId]) REFERENCES [Employees] ([Id]),
        CONSTRAINT [FK_EmployeeJobTransfer_JobTransfers_JobTransfersId] FOREIGN KEY ([JobTransfersId]) REFERENCES [JobTransfers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Certificates_UniversityId] ON [Certificates] ([UniversityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_EmployeeJobTransfer_JobTransfersId] ON [EmployeeJobTransfer] ([JobTransfersId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Employees_CertificateId] ON [Employees] ([CertificateId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Employees_GenderId] ON [Employees] ([GenderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Employees_NationalityId] ON [Employees] ([NationalityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Employees_RaceId] ON [Employees] ([RaceId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Employees_RecruitmentId] ON [Employees] ([RecruitmentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Employees_ReligionId] ON [Employees] ([ReligionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Employees_StatusId] ON [Employees] ([StatusId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_GovernmentalInstitutes_ClassificationId] ON [GovernmentalInstitutes] ([ClassificationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_GovernmentalInstitutes_LocationId] ON [GovernmentalInstitutes] ([LocationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_GovernmentalInstitutes_ParentGovernmentalInstituteId] ON [GovernmentalInstitutes] ([ParentGovernmentalInstituteId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_JobTitles_ClassificationId] ON [JobTitles] ([ClassificationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_JobTitles_EmployeeId] ON [JobTitles] ([EmployeeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_JobTitles_GovernmentalInstituteClassificationId] ON [JobTitles] ([GovernmentalInstituteClassificationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_JobTitles_GradeId] ON [JobTitles] ([GradeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_JobTitles_JobTitleClassificationId] ON [JobTitles] ([JobTitleClassificationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_JobTitles_JobTitleId] ON [JobTitles] ([JobTitleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_JobTitles_LevelId] ON [JobTitles] ([LevelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_JobTitles_ParentJobTitleId] ON [JobTitles] ([ParentJobTitleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_JobTransfers_DestinationLevelId] ON [JobTransfers] ([DestinationLevelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Levels_EmployeeId] ON [Levels] ([EmployeeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Levels_LocationId] ON [Levels] ([LocationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Levels_ParentLevelId] ON [Levels] ([ParentLevelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Locations_LocationClassificationId] ON [Locations] ([LocationClassificationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Locations_ParentLocationId] ON [Locations] ([ParentLocationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    CREATE INDEX [IX_Salary_EmployeeId] ON [Salary] ([EmployeeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704131923_initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240704131923_initial', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704144906_update_7_4_1'
)
BEGIN
    ALTER TABLE [JobTitles] DROP CONSTRAINT [FK_JobTitles_JobTitles_JobTitleId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704144906_update_7_4_1'
)
BEGIN
    ALTER TABLE [JobTitles] DROP CONSTRAINT [FK_JobTitles_JobTitles_ParentJobTitleId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704144906_update_7_4_1'
)
BEGIN
    DROP INDEX [IX_JobTitles_JobTitleId] ON [JobTitles];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704144906_update_7_4_1'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[JobTitles]') AND [c].[name] = N'JobTitleId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [JobTitles] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [JobTitles] DROP COLUMN [JobTitleId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704144906_update_7_4_1'
)
BEGIN
    ALTER TABLE [JobTitles] ADD CONSTRAINT [FK_JobTitles_JobTitles_ParentJobTitleId] FOREIGN KEY ([ParentJobTitleId]) REFERENCES [JobTitles] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704144906_update_7_4_1'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240704144906_update_7_4_1', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705075903_correctRelation'
)
BEGIN
    ALTER TABLE [JobTitles] DROP CONSTRAINT [FK_JobTitles_GovernmentalInstituteClassification_GovernmentalInstituteClassificationId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705075903_correctRelation'
)
BEGIN
    DROP INDEX [IX_JobTitles_GovernmentalInstituteClassificationId] ON [JobTitles];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705075903_correctRelation'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[JobTitles]') AND [c].[name] = N'GovernmentalInstituteClassificationId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [JobTitles] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [JobTitles] DROP COLUMN [GovernmentalInstituteClassificationId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705075903_correctRelation'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240705075903_correctRelation', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    ALTER TABLE [Employees] DROP CONSTRAINT [FK_Employees_Certificates_CertificateId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    ALTER TABLE [JobTitles] DROP CONSTRAINT [FK_JobTitles_Employees_EmployeeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    ALTER TABLE [JobTitles] DROP CONSTRAINT [FK_JobTitles_Levels_LevelId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    ALTER TABLE [Levels] DROP CONSTRAINT [FK_Levels_Employees_EmployeeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    ALTER TABLE [Salary] DROP CONSTRAINT [FK_Salary_Employees_EmployeeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    DROP INDEX [IX_Salary_EmployeeId] ON [Salary];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    DROP INDEX [IX_Levels_EmployeeId] ON [Levels];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    DROP INDEX [IX_JobTitles_EmployeeId] ON [JobTitles];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    DROP INDEX [IX_JobTitles_LevelId] ON [JobTitles];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    DROP INDEX [IX_Employees_CertificateId] ON [Employees];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Salary]') AND [c].[name] = N'EmployeeId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Salary] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Salary] DROP COLUMN [EmployeeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Levels]') AND [c].[name] = N'EmployeeId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Levels] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Levels] DROP COLUMN [EmployeeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[JobTitles]') AND [c].[name] = N'EmployeeId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [JobTitles] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [JobTitles] DROP COLUMN [EmployeeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[JobTitles]') AND [c].[name] = N'LevelId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [JobTitles] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [JobTitles] DROP COLUMN [LevelId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'CertificateId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Employees] DROP COLUMN [CertificateId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    ALTER TABLE [JobTransfers] ADD [LevelId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE TABLE [CertificateEmployee] (
        [CertificatesId] int NOT NULL,
        [EmployeesId] int NOT NULL,
        CONSTRAINT [PK_CertificateEmployee] PRIMARY KEY ([CertificatesId], [EmployeesId]),
        CONSTRAINT [FK_CertificateEmployee_Certificates_CertificatesId] FOREIGN KEY ([CertificatesId]) REFERENCES [Certificates] ([Id]),
        CONSTRAINT [FK_CertificateEmployee_Employees_EmployeesId] FOREIGN KEY ([EmployeesId]) REFERENCES [Employees] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE TABLE [EmployeeJobTitle] (
        [EmployeesId] int NOT NULL,
        [JobTitlesId] int NOT NULL,
        CONSTRAINT [PK_EmployeeJobTitle] PRIMARY KEY ([EmployeesId], [JobTitlesId]),
        CONSTRAINT [FK_EmployeeJobTitle_Employees_EmployeesId] FOREIGN KEY ([EmployeesId]) REFERENCES [Employees] ([Id]),
        CONSTRAINT [FK_EmployeeJobTitle_JobTitles_JobTitlesId] FOREIGN KEY ([JobTitlesId]) REFERENCES [JobTitles] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE TABLE [EmployeeLevel] (
        [EmployeesId] int NOT NULL,
        [LevelsId] int NOT NULL,
        CONSTRAINT [PK_EmployeeLevel] PRIMARY KEY ([EmployeesId], [LevelsId]),
        CONSTRAINT [FK_EmployeeLevel_Employees_EmployeesId] FOREIGN KEY ([EmployeesId]) REFERENCES [Employees] ([Id]),
        CONSTRAINT [FK_EmployeeLevel_Levels_LevelsId] FOREIGN KEY ([LevelsId]) REFERENCES [Levels] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE TABLE [EmployeeSalary] (
        [EmployeesId] int NOT NULL,
        [SalariesId] int NOT NULL,
        CONSTRAINT [PK_EmployeeSalary] PRIMARY KEY ([EmployeesId], [SalariesId]),
        CONSTRAINT [FK_EmployeeSalary_Employees_EmployeesId] FOREIGN KEY ([EmployeesId]) REFERENCES [Employees] ([Id]),
        CONSTRAINT [FK_EmployeeSalary_Salary_SalariesId] FOREIGN KEY ([SalariesId]) REFERENCES [Salary] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE TABLE [JobTitleLevel] (
        [JobTitlesId] int NOT NULL,
        [LevelsId] int NOT NULL,
        CONSTRAINT [PK_JobTitleLevel] PRIMARY KEY ([JobTitlesId], [LevelsId]),
        CONSTRAINT [FK_JobTitleLevel_JobTitles_JobTitlesId] FOREIGN KEY ([JobTitlesId]) REFERENCES [JobTitles] ([Id]),
        CONSTRAINT [FK_JobTitleLevel_Levels_LevelsId] FOREIGN KEY ([LevelsId]) REFERENCES [Levels] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE INDEX [IX_JobTransfers_LevelId] ON [JobTransfers] ([LevelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE INDEX [IX_CertificateEmployee_EmployeesId] ON [CertificateEmployee] ([EmployeesId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE INDEX [IX_EmployeeJobTitle_JobTitlesId] ON [EmployeeJobTitle] ([JobTitlesId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE INDEX [IX_EmployeeLevel_LevelsId] ON [EmployeeLevel] ([LevelsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE INDEX [IX_EmployeeSalary_SalariesId] ON [EmployeeSalary] ([SalariesId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    CREATE INDEX [IX_JobTitleLevel_LevelsId] ON [JobTitleLevel] ([LevelsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    ALTER TABLE [JobTransfers] ADD CONSTRAINT [FK_JobTransfers_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Levels] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705085756_AddNewRelations'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240705085756_AddNewRelations', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705130335_AddPromotions'
)
BEGIN
    CREATE TABLE [Promotions] (
        [Id] int NOT NULL IDENTITY,
        [ReferenceNo] nvarchar(max) NOT NULL,
        [ReferenceDate] datetime2 NOT NULL,
        [Subject] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Promotions] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705130335_AddPromotions'
)
BEGIN
    CREATE TABLE [EmployeePromotion] (
        [EmployeesId] int NOT NULL,
        [PromotionsId] int NOT NULL,
        CONSTRAINT [PK_EmployeePromotion] PRIMARY KEY ([EmployeesId], [PromotionsId]),
        CONSTRAINT [FK_EmployeePromotion_Employees_EmployeesId] FOREIGN KEY ([EmployeesId]) REFERENCES [Employees] ([Id]),
        CONSTRAINT [FK_EmployeePromotion_Promotions_PromotionsId] FOREIGN KEY ([PromotionsId]) REFERENCES [Promotions] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705130335_AddPromotions'
)
BEGIN
    CREATE INDEX [IX_EmployeePromotion_PromotionsId] ON [EmployeePromotion] ([PromotionsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705130335_AddPromotions'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240705130335_AddPromotions', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705143816_AddEmployeeJobTitles'
)
BEGIN
    DROP TABLE [EmployeeJobTitle];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705143816_AddEmployeeJobTitles'
)
BEGIN
    CREATE TABLE [EmployeeJobTitles] (
        [Id] int NOT NULL IDENTITY,
        [EmployeeId] int NOT NULL,
        [JobTitleId] int NOT NULL,
        [ReferenceNo] nvarchar(max) NULL,
        [ReferenceDate] datetime2 NOT NULL,
        [JobTitleAssignedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_EmployeeJobTitles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_EmployeeJobTitles_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]),
        CONSTRAINT [FK_EmployeeJobTitles_JobTitles_JobTitleId] FOREIGN KEY ([JobTitleId]) REFERENCES [JobTitles] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705143816_AddEmployeeJobTitles'
)
BEGIN
    CREATE INDEX [IX_EmployeeJobTitles_EmployeeId] ON [EmployeeJobTitles] ([EmployeeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705143816_AddEmployeeJobTitles'
)
BEGIN
    CREATE INDEX [IX_EmployeeJobTitles_JobTitleId] ON [EmployeeJobTitles] ([JobTitleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705143816_AddEmployeeJobTitles'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240705143816_AddEmployeeJobTitles', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705210038_AddAllClassifications'
)
BEGIN
    ALTER TABLE [Employees] DROP CONSTRAINT [FK_Employees_Religiones_ReligionId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705210038_AddAllClassifications'
)
BEGIN
    DROP TABLE [Religiones];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705210038_AddAllClassifications'
)
BEGIN
    ALTER TABLE [JobTitleClassifications] ADD [BaghdadSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705210038_AddAllClassifications'
)
BEGIN
    ALTER TABLE [JobTitleClassifications] ADD [KirkukSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705210038_AddAllClassifications'
)
BEGIN
    ALTER TABLE [Genders] ADD [BaghdadSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705210038_AddAllClassifications'
)
BEGIN
    ALTER TABLE [Genders] ADD [KirkukSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705210038_AddAllClassifications'
)
BEGIN
    CREATE TABLE [Religions] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Religions] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705210038_AddAllClassifications'
)
BEGIN
    ALTER TABLE [Employees] ADD CONSTRAINT [FK_Employees_Religions_ReligionId] FOREIGN KEY ([ReligionId]) REFERENCES [Religions] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705210038_AddAllClassifications'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240705210038_AddAllClassifications', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705211443_AddKrkBGDSymbols'
)
BEGIN
    ALTER TABLE [Religions] ADD [BaghdadSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705211443_AddKrkBGDSymbols'
)
BEGIN
    ALTER TABLE [Religions] ADD [KirkukSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705211443_AddKrkBGDSymbols'
)
BEGIN
    ALTER TABLE [Races] ADD [BaghdadSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705211443_AddKrkBGDSymbols'
)
BEGIN
    ALTER TABLE [Races] ADD [KirkukSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705211443_AddKrkBGDSymbols'
)
BEGIN
    ALTER TABLE [Nationalities] ADD [BaghdadSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705211443_AddKrkBGDSymbols'
)
BEGIN
    ALTER TABLE [Nationalities] ADD [KirkukSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705211443_AddKrkBGDSymbols'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240705211443_AddKrkBGDSymbols', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705225718_AddKrkBGDSymbols2'
)
BEGIN
    ALTER TABLE [LocationClassifications] ADD [BaghdadSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705225718_AddKrkBGDSymbols2'
)
BEGIN
    ALTER TABLE [LocationClassifications] ADD [KirkukSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705225718_AddKrkBGDSymbols2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240705225718_AddKrkBGDSymbols2', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706001843_Update_7_6'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[JobTransfers]') AND [c].[name] = N'DestinationLevelId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [JobTransfers] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [JobTransfers] ALTER COLUMN [DestinationLevelId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706001843_Update_7_6'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240706001843_Update_7_6', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706004902_AddCascadeToJobTransfers'
)
BEGIN
    ALTER TABLE [EmployeeJobTransfer] DROP CONSTRAINT [FK_EmployeeJobTransfer_JobTransfers_JobTransfersId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706004902_AddCascadeToJobTransfers'
)
BEGIN
    ALTER TABLE [EmployeeJobTransfer] ADD CONSTRAINT [FK_EmployeeJobTransfer_JobTransfers_JobTransfersId] FOREIGN KEY ([JobTransfersId]) REFERENCES [JobTransfers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706004902_AddCascadeToJobTransfers'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240706004902_AddCascadeToJobTransfers', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706162706_AddLevelClassification'
)
BEGIN
    ALTER TABLE [Levels] ADD [ClassificationId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706162706_AddLevelClassification'
)
BEGIN
    CREATE TABLE [LevelClassifications] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Rank] int NOT NULL,
        [KirkukSymbol] nvarchar(max) NULL,
        [BaghdadSymbol] nvarchar(max) NULL,
        CONSTRAINT [PK_LevelClassifications] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706162706_AddLevelClassification'
)
BEGIN
    CREATE INDEX [IX_Levels_ClassificationId] ON [Levels] ([ClassificationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706162706_AddLevelClassification'
)
BEGIN
    ALTER TABLE [Levels] ADD CONSTRAINT [FK_Levels_LevelClassifications_ClassificationId] FOREIGN KEY ([ClassificationId]) REFERENCES [LevelClassifications] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706162706_AddLevelClassification'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240706162706_AddLevelClassification', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706220258_UpdateLocations'
)
BEGIN
    ALTER TABLE [Locations] DROP CONSTRAINT [FK_Locations_LocationClassifications_LocationClassificationId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706220258_UpdateLocations'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Locations]') AND [c].[name] = N'Type');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Locations] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Locations] DROP COLUMN [Type];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706220258_UpdateLocations'
)
BEGIN
    EXEC sp_rename N'[Locations].[LocationClassificationId]', N'ClassificationId', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706220258_UpdateLocations'
)
BEGIN
    EXEC sp_rename N'[Locations].[IX_Locations_LocationClassificationId]', N'IX_Locations_ClassificationId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706220258_UpdateLocations'
)
BEGIN
    ALTER TABLE [Locations] ADD CONSTRAINT [FK_Locations_LocationClassifications_ClassificationId] FOREIGN KEY ([ClassificationId]) REFERENCES [LocationClassifications] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706220258_UpdateLocations'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240706220258_UpdateLocations', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240729064815_update7_29'
)
BEGIN
    ALTER TABLE [GovernmentalInstitutes] DROP CONSTRAINT [FK_GovernmentalInstitutes_GovernmentalInstituteClassification_ClassificationId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240729064815_update7_29'
)
BEGIN
    ALTER TABLE [GovernmentalInstituteClassification] DROP CONSTRAINT [PK_GovernmentalInstituteClassification];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240729064815_update7_29'
)
BEGIN
    EXEC sp_rename N'[GovernmentalInstituteClassification]', N'GovernmentalInstituteClassifications';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240729064815_update7_29'
)
BEGIN
    ALTER TABLE [GovernmentalInstituteClassifications] ADD CONSTRAINT [PK_GovernmentalInstituteClassifications] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240729064815_update7_29'
)
BEGIN
    ALTER TABLE [GovernmentalInstitutes] ADD CONSTRAINT [FK_GovernmentalInstitutes_GovernmentalInstituteClassifications_ClassificationId] FOREIGN KEY ([ClassificationId]) REFERENCES [GovernmentalInstituteClassifications] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240729064815_update7_29'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240729064815_update7_29', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240804065310_update8_4'
)
BEGIN
    ALTER TABLE [GovernmentalInstituteClassifications] ADD [BaghdadSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240804065310_update8_4'
)
BEGIN
    ALTER TABLE [GovernmentalInstituteClassifications] ADD [KirkukSymbol] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240804065310_update8_4'
)
BEGIN
    ALTER TABLE [Employees] ADD [GovernmentalInstituteClassificationId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240804065310_update8_4'
)
BEGIN
    CREATE INDEX [IX_Employees_GovernmentalInstituteClassificationId] ON [Employees] ([GovernmentalInstituteClassificationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240804065310_update8_4'
)
BEGIN
    ALTER TABLE [Employees] ADD CONSTRAINT [FK_Employees_GovernmentalInstituteClassifications_GovernmentalInstituteClassificationId] FOREIGN KEY ([GovernmentalInstituteClassificationId]) REFERENCES [GovernmentalInstituteClassifications] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240804065310_update8_4'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240804065310_update8_4', N'8.0.4');
END;
GO

COMMIT;
GO