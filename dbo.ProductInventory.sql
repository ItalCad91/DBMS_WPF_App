CREATE TABLE [dbo].[ProductInventory] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [Name]              VARCHAR (50)    NULL,
    [LotNumber]         VARCHAR (50)    NULL,
    [AvailableQuantity] INT             NULL,
    [Price]             DECIMAL (10, 2) NULL,
    [QuantitySold]      INT             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

