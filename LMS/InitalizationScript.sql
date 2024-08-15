SET IDENTITY_INSERT [dbo].[BookCategories] ON
INSERT INTO [dbo].[BookCategories] ([Id], [CategoryName]) VALUES (1, N'Fantasy')
INSERT INTO [dbo].[BookCategories] ([Id], [CategoryName]) VALUES (2, N'Sci-Fi')
INSERT INTO [dbo].[BookCategories] ([Id], [CategoryName]) VALUES (3, N'Horror')
INSERT INTO [dbo].[BookCategories] ([Id], [CategoryName]) VALUES (4, N'Historical')
INSERT INTO [dbo].[BookCategories] ([Id], [CategoryName]) VALUES (5, N'Romance')
SET IDENTITY_INSERT [dbo].[BookCategories] OFF

SET IDENTITY_INSERT [dbo].[Books] ON
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [PublicationDate], [ISBN], [BookCategoryId], [TotalCopies], [AvailableCopies]) VALUES (1, N'The Fellowship of the Ring', N'JRR Tolkein', N'HarperCollins', N'2024-08-15 00:27:42', N'123-456-789', 1, 1, 1)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [PublicationDate], [ISBN], [BookCategoryId], [TotalCopies], [AvailableCopies]) VALUES (2, N'The Two Towers', N'JRR Tolkein', N'HarperCollins', N'2024-08-15 00:27:42', N'123-456-987', 1, 1, 1)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [PublicationDate], [ISBN], [BookCategoryId], [TotalCopies], [AvailableCopies]) VALUES (3, N'The Return of the King', N'JRR Tolkein', N'HarperCollins', N'2024-08-15 00:27:42', N'123-654-987', 1, 1, 1)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [PublicationDate], [ISBN], [BookCategoryId], [TotalCopies], [AvailableCopies]) VALUES (4, N'Unfinished Tales', N'JRR Tolkein', N'HarperCollins', N'2024-08-15 00:27:42', N'321-654-987', 1, 1, 1)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [PublicationDate], [ISBN], [BookCategoryId], [TotalCopies], [AvailableCopies]) VALUES (5, N'The Tower', N'Stephen King', N'HorrorRific', N'2024-08-15 00:27:42', N'321-654-123', 3, 1, 1)
SET IDENTITY_INSERT [dbo].[Books] OFF

