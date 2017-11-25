CREATE TABLE [dbo].[MovieModels] (
    [id]          INT  NOT NULL IDENTITY,
    [type]        NVARCHAR (40)   NOT NULL,
    [name]        NVARCHAR (40)   NOT NULL,
    [poster]      NVARCHAR (MAX)  NOT NULL,
    [area]        NVARCHAR (50)  NOT NULL,
    [releaseTime] DATETIME        NOT NULL,
    [price]       DECIMAL (18, 2) NOT NULL,
    [remarks]     NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_dbo.MovieModels] PRIMARY KEY CLUSTERED ([id] ASC)
);

