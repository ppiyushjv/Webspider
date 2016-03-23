-- Script Date: 27-08-2015 21:34  - ErikEJ.SqlCeScripting version 3.5.2.42
CREATE TABLE [SecLockManufacturer] (
  [Code] nvarchar(100) NOT NULL
, [Name] nvarchar(100) NOT NULL
, [ImagePath] nvarchar(100) NOT NULL
, [Url] nvarchar(100) NOT NULL
);
GO
ALTER TABLE [SecLockManufacturer] ADD CONSTRAINT [PK_SecLockManufacturer] PRIMARY KEY ([Code]);
GO

-- Script Date: 27-08-2015 22:09  - ErikEJ.SqlCeScripting version 3.5.2.42
CREATE TABLE [SecLockManufacturerSeries] (
  [ID] bigint IDENTITY (1,1) NOT NULL
, [ManufacturerCode] nvarchar(100) NOT NULL
, [Name] nvarchar(100) NOT NULL
);
GO
ALTER TABLE [SecLockManufacturerSeries] ADD CONSTRAINT [PK_SecLockManufacturerSeries] PRIMARY KEY ([ID]);
GO
-- Script Date: 27-08-2015 23:08  - ErikEJ.SqlCeScripting version 3.5.2.42
CREATE TABLE [SecLockCategory] (
  [Code] nvarchar(100) NOT NULL
, [Name] nvarchar(100) NOT NULL
);
GO
ALTER TABLE [SecLockCategory] ADD CONSTRAINT [PK_SecLockCategory] PRIMARY KEY ([Code]);
GO
-- Script Date: 27-08-2015 23:30  - ErikEJ.SqlCeScripting version 3.5.2.42
CREATE TABLE [SecLockProduct] (
  [Code] nvarchar(100) NOT NULL
, [Name] nvarchar(100) NOT NULL
);
GO
ALTER TABLE [SecLockProduct] ADD CONSTRAINT [PK_SecLockProduct] PRIMARY KEY ([Code]);
GO