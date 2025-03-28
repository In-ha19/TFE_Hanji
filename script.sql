USE [master]
GO
/****** Object:  Database [GestionnaireCollection]    Script Date: 25-03-25 10:35:29 ******/
CREATE DATABASE [GestionnaireCollection]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GestionnaireCollection', FILENAME = N'C:\Users\bombl\GestionnaireCollection.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GestionnaireCollection_log', FILENAME = N'C:\Users\bombl\GestionnaireCollection_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GestionnaireCollection].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GestionnaireCollection] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET ARITHABORT OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [GestionnaireCollection] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GestionnaireCollection] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GestionnaireCollection] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GestionnaireCollection] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GestionnaireCollection] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [GestionnaireCollection] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GestionnaireCollection] SET  MULTI_USER 
GO
ALTER DATABASE [GestionnaireCollection] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GestionnaireCollection] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GestionnaireCollection] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GestionnaireCollection] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GestionnaireCollection] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GestionnaireCollection] SET QUERY_STORE = OFF
GO
USE [GestionnaireCollection]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 25-03-25 10:35:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Articles]    Script Date: 25-03-25 10:35:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Edition] [nvarchar](max) NULL,
	[Isbn] [nvarchar](450) NULL,
	[Autor_name] [nvarchar](max) NULL,
	[Date] [nvarchar](max) NULL,
	[Summary] [nvarchar](max) NULL,
	[Is_active] [bit] NOT NULL,
	[Price] [real] NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 25-03-25 10:35:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 25-03-25 10:35:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 25-03-25 10:35:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 25-03-25 10:35:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 25-03-25 10:35:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 25-03-25 10:35:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Login] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 25-03-25 10:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 25-03-25 10:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Is_active] [bit] NOT NULL,
	[Is_principal] [bit] NOT NULL,
	[Parent_id] [nvarchar](450) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category_Articles]    Script Date: 25-03-25 10:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category_Articles](
	[CategoryId] [nvarchar](450) NOT NULL,
	[ArticleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Category_Articles] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC,
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Collections]    Script Date: 25-03-25 10:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collections](
	[UserId] [nvarchar](450) NOT NULL,
	[ArticleId] [nvarchar](450) NOT NULL,
	[Note] [real] NULL,
	[Statut] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Purchased_Price] [real] NULL,
	[Purchased_Date] [datetime2](7) NULL,
 CONSTRAINT [PK_Collections] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Families]    Script Date: 25-03-25 10:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Families](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Families] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Family_Users]    Script Date: 25-03-25 10:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Family_Users](
	[FamilyId] [nvarchar](450) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Is_manager] [bit] NOT NULL,
 CONSTRAINT [PK_Family_Users] PRIMARY KEY CLUSTERED 
(
	[FamilyId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 25-03-25 10:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Root] [nvarchar](max) NOT NULL,
	[ArticleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberShip_Families]    Script Date: 25-03-25 10:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberShip_Families](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequesterId] [nvarchar](450) NOT NULL,
	[FamilyId] [nvarchar](450) NOT NULL,
	[Statut] [int] NOT NULL,
 CONSTRAINT [PK_MemberShip_Families] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 25-03-25 10:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Statut] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[MessageObjet] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notepads]    Script Date: 25-03-25 10:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notepads](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Date_reminder] [datetime2](7) NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Titre] [nvarchar](max) NOT NULL,
	[IsEmailSent] [bit] NOT NULL,
 CONSTRAINT [PK_Notepads] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241018110249_InitialCreate', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241018155157_LoginInAppUser', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241019103300_AddUniqueConstraints', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241025063828_changeuserName', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241101075954_isManagerInDB', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241101100557_MemberShip_Family', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241101102531_changementMemberShip', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241115114309_imageInarticle', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241122151907_nullImageInArticle', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241123105055_PriceArticleFloatNotInt', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241123163525_EnleverimagedeArticle', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241123164114_ArticlIduniqueImage', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241214135337_Alter_Delet_cascadeMemberShip', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241221163402_DeleteCascadeArticleId', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250111142013_MessageOject', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250116141555_Titre_Notepad', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250117111716_IsEmailSent_Notepad', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250320081932_UNIQUEfAMILYnAME', N'9.0.1')
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'33886d47-1721-4300-8bdd-bcf75bb771b8', N'Bibliophile princess 1', N' Nobi Nobi', NULL, NULL, NULL, N'Jeune fille issue de l’aristocratie, Elianna est passionnée par les livres. Elle est la promise du prince héritier, son altesse royale Christopher. Sauf qu''en réalité, ces fiançailles ne sont qu''une façade : en échange de cette union, le prince laisse Elianna accéder à la bibliothèque royale comme bon lui semble. Quelques années plus tard, la jeune femme apprend que Christopher aurait une autre prétendante... Elianna se prépare donc à voir ses fiançailles annulées. Mais la situation va se révéler bien plus complexe qu''elle ne l''imagine…
Le rideau se lève sur une romance fantasy, et sur les confusions d’une jeune aristocrate passionnée par la lecture !', 1, 7.25)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'4619c4ec-31e2-4604-91d4-6cc5b7a2538e', N'Des coeurs & du love', N'Marabout', NULL, N'Claire Chicoine', NULL, NULL, 1, 9.6)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'73ee3e0b-e869-4646-a778-3d4b06fd4ba9', N'Untouchable Lady 1', N' kbooks', NULL, N'Yong Doosik', N'2024-01-21', N'Ricardo, un noble, supplie sa soeur Hillis de mourir pour sauver leur demi-soeur Gabrielle, avec qui elle ne partage pas la moindre goutte de sang. Hillis comprend qu''elle n''a jamais été aimée autant que cette dernière. Après être morte sept fois, et pour tenter d''échapper à son tragique destin, la douce et timide Hillis laisse place à une version d''elle implacable et avide de vengeance !', 1, 14.95)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'85f37621-aee7-4bba-96af-15088f33056a', N'Coudre à la machine: Le B.A.-ba pour débuter en couture', N'Dessain et Tolra', NULL, N'Kate Haxell ', N'2021-04-07', NULL, 1, 13.9)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'94bd02b6-1401-4c85-a099-e7ead437faa8', N'Shitsuji-sama no Okiniiri - Collection complète', NULL, NULL, NULL, NULL, N'Himura Ryou, after loosing her parents, is taken in by her grandparents who are from a distinguished family. Through them, she is transfers to the famous Souseikan Academy. First day at school, Ryou receives guidance from a guy in a tuxedo when she got lost in the vast garden of the academy. When she learns that he''s Kanzawa Hakuou of the shutsuji apprentice course, which is also known as "B class", Ryou...!?
(Source: Esth&eacute;tique)', 1, NULL)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'9dd2c360-b66e-48bd-a387-885d4c6536c8', N'My Happy Marriage Tome 2', NULL, NULL, NULL, NULL, N'Ils redoutaient leur union, elle sera la clé de leur bonheur...

Un tout premier contact avec la gentillesse d''autrui...

Miyo Saimori descend d''une longue lignée de nobles dotés de pouvoirs surnaturels, alors qu''elle-même n''en a pas. Sa demi-sœur, qui a, elle, hérité d''un don, la traite comme une servante. Ses parents ne lui témoignent aucun amour et personne ne se soucie d''elle.

Considérée comme une nuisance par tout son entourage, Miyo est envoyée dans la famille Kudô pour épouser leur chef, réputé cruel et sans cœur. Elle réussit pourtant à capter l''attention de Kiyoka Kudô. Son promis a beau s''être attaché à elle, Miyo n''a aucun pouvoir et ne s''estime pas digne de l''épouser...

Découvrez un savant mélange de fantasy, de Japon ancien et de romance dans cette intrigue touchante menée tambour battant, et qui s''ouvre sur des fiançailles.', 1, 7.2)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'ae6d4b34-36a8-480e-a985-2d35b1224c6d', N'Le grand livre des tambours brodés', N'de Saxe', NULL, NULL, NULL, NULL, 1, 22)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'b3b45d34-f4e7-4e23-8b02-9e09df6ae87a', N'Les carnets de l''apothicaire 1', NULL, NULL, NULL, NULL, NULL, 1, 7.95)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'c47928a3-b378-49d0-94df-6172d3d9cc58', N'Yona princesse de l''aube Tome 1', N'Pika', NULL, N'Mizuho Kusanagi', NULL, N'La flotte du Kai du Sud menace la côte de Sensui ! Yona et ses amis vont devoir trouver une stratégie pour défendre les terres de la Tribu de l''eau. Cela implique de faire équipe avec Soo-won qui se présente comme le garde du corps de Lili... Quelle va être la réaction de Hak lorsqu''il va se retrouver face à son ancien ami ?', 1, 7.2)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'e1da082a-5714-4a71-86da-b64099d5158f', N'Pandora Hearts - Collection complète', NULL, NULL, NULL, NULL, N'The air of celebration surrounding fifteen-year-old Oz Vessalius&rsquo;s coming-of-age ceremony quickly turns to horror when he is condemned for a sin about which he knows nothing. He is thrown into an eternal, inescapable prison known as the Abyss from which there is no escape. There, he meets a young girl named Alice, who is not what she seems. Now that the relentless cogs of fate have begun to turn, do they lead only to crushing despair for Oz, or is there some shred of hope for him to grasp on to?
(Source: Yen Press)

Included:
Volume 8: Pandora Hearts (pilot)
Volume 20: Extra episode: It makes all kinds
Volume 21: Extra episode: Together', 1, NULL)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'e2373a6e-0e5c-4544-a53b-8e99a4fdfe0d', N'My Happy Marriage Tome 1', NULL, NULL, NULL, NULL, N'Ils redoutaient leur union, elle sera la clé de leur bonheur...

Tout ce que je souhaite, ce n''est qu''un tout petit peu de bonheur...

Miyo Saimori descend d''une longue lignée de nobles dotés de pouvoirs surnaturels, alors qu''elle-même n''en a pas. Sa demi-sœur, qui a, elle, hérité d''un don, la traite comme une servante. Ses parents ne lui témoignent aucun amour et personne ne se soucie d''elle. Pour couronner le tout, son ami d''enfance et seul allié s''est fiancé à sa sœur pour reprendre la tête du clan Saimori.
Étant considérée comme une nuisance par tout son entourage, Miyo est envoyée dans la famille Kudô pour épouser leur chef, réputé cruel et sans cœur.

Découvrez ce savant mélange de fantasy, de Japon ancien et de romance dans cette intrigue touchante menée tambour battant, et qui s''ouvre sur des fiançailles.', 1, 7.2)
INSERT [dbo].[Articles] ([Id], [Name], [Edition], [Isbn], [Autor_name], [Date], [Summary], [Is_active], [Price]) VALUES (N'f3f3ab13-708c-44d5-bbe9-47d9f305b461', N'Suterareta Tensei Kenja: Mamono no Mori de Saikyou no Daima Teikoku wo Tsukuriageru - Collection complète', NULL, NULL, NULL, NULL, N'Belamus was once a great sage with the power of reincarnation...but as all lives must pass, so did his. He reincarnated with his memories, but was suddenly abandoned as a newborn child! Luckily for him, he was adopted by the nearby goblin tribe...but this moment of kindness may mean more for both the Goblins and Belamus than either of them know!

(Source: Kodansha USA)', 1, NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c030d785-ed2f-4dee-9da5-b74d87c04556', N'User', N'USER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c4d6c412-227e-43de-a694-eea4fb78ac4c', N'Admin', N'ADMIN', NULL)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'02d015c2-c954-4480-b8c5-04697211572d', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1139533b-005b-4715-ab4d-792a5f0e6f9f', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1c80f129-1827-4bbb-8b56-c0e6656c7809', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1decba04-5a1c-44a5-ad31-249527dbf60a', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'20d8619b-ab59-40c4-902f-bea6136cc0ca', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'326709c5-3f00-4feb-853b-20b2a7fd8a1c', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'41d1074b-6699-40fd-9706-af6956d0d41d', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'592ed203-418a-4d6f-907c-7af3fa782ea7', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6b9442b4-8750-4f2b-b566-62424bb0b257', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'72fd5da8-e3b3-469b-9031-02058060f88f', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'956a4a92-48b3-4e71-b7e9-34e9f8aa0476', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a5a2c64e-4c00-4fee-b467-7eea0a3a191c', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a8affb33-6164-47e0-8de0-b94cf346f4b2', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a9db040e-6bab-463a-ba02-bb03b34b1d71', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'afde9d6a-a452-4555-b3b1-8fbb9d6753e8', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b267870c-2bb7-4130-a1b6-c0a921ff5cf0', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'befc10d2-0744-472f-8173-6c87d46c09ab', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd03f1e0c-6aba-4c5a-b489-af64c33ebb0f', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'dc94a5ce-493a-4ce5-9b9a-8a96a8203419', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e8414ed1-570e-4138-9cb9-392c62471976', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f6651c40-8310-48d7-b411-cdb687c9bc62', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f9acd7a6-e786-4b65-b6b4-c2c96a4bc946', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fc039b95-b150-4dfb-85c1-8d16048c6e97', N'c030d785-ed2f-4dee-9da5-b74d87c04556')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6b9442b4-8750-4f2b-b566-62424bb0b257', N'c4d6c412-227e-43de-a694-eea4fb78ac4c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'72fd5da8-e3b3-469b-9031-02058060f88f', N'c4d6c412-227e-43de-a694-eea4fb78ac4c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'74279c81-2e55-475f-a221-7a2b18405299', N'c4d6c412-227e-43de-a694-eea4fb78ac4c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'dcd7dd31-2208-42fe-b5f9-78872203bf00', N'c4d6c412-227e-43de-a694-eea4fb78ac4c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f3f277bd-b5d6-4a41-a0f3-ecb102cb4a95', N'c4d6c412-227e-43de-a694-eea4fb78ac4c')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'02d015c2-c954-4480-b8c5-04697211572d', 0, 0, N'Idem', N'IDEM', N'idem@m', N'IDEM@M', 1, N'AQAAAAIAAYagAAAAEIedpnII3t2QIpRbht3t/0spCPNeuIOgZR2ekXin3X7Yh0rlHAfOyV6gFzN3C51Gpw==', N'RKYOO57B6PMNV2HT5OW774X47UD5OPEB', N'4fecf019-ec83-4db6-b047-0cae87a7ac4a', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'1139533b-005b-4715-ab4d-792a5f0e6f9f', 0, 1, N'Micha', N'MICHA', N'Micha@hotmail.com', N'MICHA@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEPBFcVtfZA40/Tvsr8cV0T3IY8yfhmOGlIS2pignd3wGrXGsXm/c7R8tbeYLhMuUMg==', N'PHLMW3WXU5Q4WJC6ODXV2F7QGR7SA6AV', N'5f72113f-db93-49de-aa0d-cb321a2bf094', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'1c80f129-1827-4bbb-8b56-c0e6656c7809', 0, 1, N'Shin', N'SHIN', N'inha2@hotmail.com', N'INHA2@HOTMAIL.COM', 0, N'AQAAAAIAAYagAAAAEHGg/oyO2PXSx/xaQYoZx3Y2Pyb2KL93cwNC+lgPiIBTyvanV0YNn8Nec2IWFgyq6w==', N'4M7SEMCWBT3K4JJITXZNJCQ4CDK544WV', N'e687aa84-915e-409f-826f-b9160a43bd40', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'1decba04-5a1c-44a5-ad31-249527dbf60a', 0, 1, N'Shin2', N'SHIN2', N'inha3@hotmail.com', N'INHA3@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEE7V5xAIAWvwHV4mQnqxYSE15DgNIKHOGufYhE1pa2zKeuDZejuDBaAi9ISWlJPTkA==', N'MQQRJA2JQ2Q37J6XXS5RVP6EOOR5NNCO', N'b42c6802-2395-404a-b008-ca5d92c5c1ab', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'20d8619b-ab59-40c4-902f-bea6136cc0ca', 0, 1, N'Laura', N'LAURA', N'laura.bombled@hotmail.com', N'LAURA.BOMBLED@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEMUD4LjFw2+NyOSzcC9KYHZVBmUkioPmDjFsAwQoBkADspVkxkPv6L69I3dPeJqNOw==', N'XAC5HRBRGNHXMRMLULQDXLDGZ4XQONH2', N'ced4bb9a-a2db-4c0b-81d2-5f0119e94d5d', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'326709c5-3f00-4feb-853b-20b2a7fd8a1c', 0, 1, N'1Test@mail.com', N'1TEST@MAIL.COM', N'1Test@mail.com', N'1TEST@MAIL.COM', 1, N'AQAAAAIAAYagAAAAEDVNs5qgslRMCszDDPILbnOsiinVx3RpmTLhKMeGV5RH/sJ2frXTpsiRh6TX2gZNIw==', N'RYTRHP32YYRIXIVXSTNER6F2HWENSB7Y', N'9af25d60-ad08-46ca-9d67-7d3a758d7189', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'41d1074b-6699-40fd-9706-af6956d0d41d', 0, 0, N'Leo', N'LEO', N'leo@hotmail.com', N'LEO@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEJ09nHgAL9egSMEUDpByUHcM3vu5TCP0W9rNtSI/uhC/loqVqlNDnV1z9Y3LMQGW/A==', N'4TQBHIAGWFMTLEINQFFKCYRMMEPYWJDW', N'38111cdb-5f84-48a6-add5-a8cb2bd5946a', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'592ed203-418a-4d6f-907c-7af3fa782ea7', 0, 0, N'Altaa', N'ALTAA', N'marc_herny_sana@msn.com', N'MARC_HERNY_SANA@MSN.COM', 1, N'AQAAAAIAAYagAAAAEJHgEeb66N04ixHj5i8Z/IWx2OgYDPB/aYefVkt/W+vYQW9WDGFlT6bkVgSH3y5IFg==', N'ABGKWMUNSDVTKMPG4IAGAANMRC36Y375', N'ec5f2de2-c51a-448d-9ee6-3171a33a0829', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'6b9442b4-8750-4f2b-b566-62424bb0b257', 1, 1, N'micha2', N'MICHA2', N'micha2@hotmail.com', N'MICHA2@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEJGaojl0joFk5vf+94VseRoaFVxeLlTvCmHruHy8tU8PNntYLZuwsaXNmZ3Ql7wrXw==', N'EPKE2GYJ4NLVBANTMUG2C6LOL4AZTWI3', N'e8a348e4-91e9-47ac-9965-53f6bc5daf4e', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'72fd5da8-e3b3-469b-9031-02058060f88f', 1, 1, N'John2@mail.com', N'JOHN2@MAIL.COM', N'John2@mail.com', N'JOHN2@MAIL.COM', 0, N'AQAAAAIAAYagAAAAEOgfcefYxXB2zlc9CuZ3tQ5qWDfHrB6wJmhb3IKzoN38Sj6ZEcQvX7M0qoK9uxYPEA==', N'SV2WASWNKJ6B53DJJTAVY3MSSJIOLBX4', N'429a89b0-e852-4872-a784-29a2d8ed2b4a', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'74231ae6-b362-4828-b735-1c1a0ea15b69', 0, 0, N'user', N'USER', N'celia.bombled@hotmail.com', N'CELIA.BOMBLED@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEMzg0Rv8gZhPVytp6Mtr3PiEO50Bp+Dhnb4Egh27k5ISQgrHS5Qf9XDk42xtHVSjtw==', N'F2JXCGO272HUHCCSQL3UUGDWZVQPQDAI', N'15bb6416-1e66-4d55-bb08-944f84de4ced', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'74279c81-2e55-475f-a221-7a2b18405299', 1, 1, N'admin', N'ADMIN', N'celine.bombled@hotmail.com', N'CELINE.BOMBLED@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAENJOVLUvvm61eQzXtQDj5ifvr4/tlW5knGsRk7dbCscHOkHyTpaP/+oePa7QZy81xA==', N'HSSODBJT5AKYVYAEGTBNTQAV53EC3CHZ', N'6edc3259-561c-423c-a623-ef678ee71762', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'956a4a92-48b3-4e71-b7e9-34e9f8aa0476', 0, 0, N'Papa1', N'PAPA1', N'papa1@hotmail.com', N'PAPA1@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEOYkHkeZFbUMlg6+tGKMvn+NjH8G6gKe2iE4BWOfgk/QzTRkgI4/wzjir9QAG2mrFA==', N'GWYYMY7XPKQ3O3FZFYYBAYLBAMAQBRWB', N'b79e003a-b281-4d1a-bbfc-bd55d6472655', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'a5a2c64e-4c00-4fee-b467-7eea0a3a191c', 0, 0, N'Altarion', N'ALTARION', N'marc_herny_san@msn.com', N'MARC_HERNY_SAN@MSN.COM', 1, N'AQAAAAIAAYagAAAAELrPwxsmSSSn9TvaNWIBlwOnYviVWROMBjlyX0rhrHE1jrgrtBg1Md6lpQKJs7xjjg==', N'64SRXRP7OWWDT6VNTVWKNX5GIFNQ4NJA', N'0dff05bf-f8c2-453a-b54c-20cafcd17fae', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'a8affb33-6164-47e0-8de0-b94cf346f4b2', 0, 0, N'Maman', N'MAMAN', N'maman@hotmail.com', N'MAMAN@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEChQ3v7N1JHbNDAfavREcdVtFmEHsHlgo+gHPfYChNAb98NisWJGIIxHBuO4PaptTA==', N'CPWA4FIOFFDXDVCZJQPH7QAVU5SD6LTJ', N'd22312d0-173e-490a-8d3f-703433a984c3', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'a9db040e-6bab-463a-ba02-bb03b34b1d71', 0, 1, N'Test77', N'TEST77', N'test@hot.com', N'TEST@HOT.COM', 1, N'AQAAAAIAAYagAAAAEG/AfY9Hkl93PV0uOv7lwYraf1Pf022vqHrGKSrREiuguQ0kXBbUnnZkqYcnW7PXVQ==', N'K6S3CTE5SABOSK5S5VO7NTWPKIB5EUHW', N'f3cfaae6-58d8-4276-86b2-d188081e419b', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'afde9d6a-a452-4555-b3b1-8fbb9d6753e8', 0, 1, N'Moi', N'MOI', N'moi@hotmail.com', N'MOI@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEJ507nT0qFrqa8IzzjCGkuhBxegCed+z4dSjvtHJmsPW+c4s5PDqwFgpyq2T6lFRpg==', N'EATO5WOWON7QFQDXNQ734AF6VGCLOAMH', N'a505b773-f6d6-4905-9d81-22c7693ad631', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'b267870c-2bb7-4130-a1b6-c0a921ff5cf0', 0, 1, N'marc_henry_sana@msn.com', N'MARC_HENRY_SANA@MSN.COM', N'marc_henry_sana@msn.com', N'MARC_HENRY_SANA@MSN.COM', 0, N'AQAAAAIAAYagAAAAEJT/Q/UuBmqIC/dZeoQWPMev7N2gRDzCEHcX3bI3i/APeaXX4Ly4mPKBlhsf7TrxIA==', N'D4IRY7JXBLDKITE64MPIJVWHLJQXBMUT', N'4292c2a6-3209-4230-89c1-c4ac08586fe7', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'befc10d2-0744-472f-8173-6c87d46c09ab', 0, 1, N'Test2', N'TEST2', N'test.bombled@hot', N'TEST.BOMBLED@HOT', 0, N'AQAAAAIAAYagAAAAEEK3uU0HdgREfQfwBLuuc8VtlVr2rd69Ddozip+KwJud2tfQIkEB2ntutvsg0N4wrQ==', N'U2UP7EDY4CPCWO64RFQH5AASERZXMYQG', N'4a8bc26a-1c8a-4c2a-bffb-99af402a8ebd', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'd03f1e0c-6aba-4c5a-b489-af64c33ebb0f', 0, 1, N'Test47', N'TEST47', N'tt@re', N'TT@RE', 0, N'AQAAAAIAAYagAAAAEMdp516tPGBFCrTL3SXNTN8xaeljXcq/qYeZwK8IzHHXs4PYXkwMpHM3+rcCkiSskw==', N'56OM6EVROKZDYOLLYB5KGHYTJYLGDZUV', N'5b69fc0a-5516-46b9-a935-6c02dbef5e54', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'dc94a5ce-493a-4ce5-9b9a-8a96a8203419', 0, 1, N'Test45', N'TEST45', N'tt@h', N'TT@H', 0, N'AQAAAAIAAYagAAAAEM7SEdMy+INqR7UDvumQYQ+USunzpQIkhcxsRYpI+YC1OEzHVY2CZ54hdZidGyPxGw==', N'2PEV4AXZU6Y7EI5SRA3IOJRLC2VJGSCG', N'a1af15fe-589a-4aee-9c78-ff77abb6d541', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'dcd7dd31-2208-42fe-b5f9-78872203bf00', 1, 0, N'John1@mail.com', N'JOHN1@MAIL.COM', N'John1@mail.com', N'JOHN1@MAIL.COM', 0, N'AQAAAAIAAYagAAAAEGYXN7EW3uxSR0dd1Kh0MmixNMmqehGY4Atl6UxEfk+e44q7zdCHLcbsXrLlsHrl5g==', N'OFU3PDAYMB6FAB2GVTRKDY24HEOH7QZZ', N'074a87a1-e0cd-4f13-ab97-6b24aa0d3e81', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'e8414ed1-570e-4138-9cb9-392c62471976', 0, 1, N'Test', N'TEST', N'test@hotmail.com', N'TEST@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEGyPQN2WxJqmnbt6d8T+cOV9TB/d/ARW4Q2EcYgLvoZfmxLaStV5gAeGr9DMCE475g==', N'GLSWW22YIKXZX5TQ2LR2LL3I6NW6TYT2', N'22653f55-8357-45b2-b3fb-49848df09800', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'f3f277bd-b5d6-4a41-a0f3-ecb102cb4a95', 1, 1, N'admin2', N'ADMIN2', N'admin@mail.be', N'ADMIN@MAIL.BE', 1, N'AQAAAAIAAYagAAAAEGuqlXNSDgUPDVXpqpOa26ZSHMC1QFxzVlcSDQ/0OiAz1gK2TjM5lwviy7f9iuLdsw==', N'7KH25C2MDGUCMJS6UUEV7M26VNCTHVOW', N'a58edd32-6940-42cc-ae16-069759b73a35', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'f6651c40-8310-48d7-b411-cdb687c9bc62', 0, 0, N'Papa', N'PAPA', N'papa@hotmail.com', N'PAPA@HOTMAIL.COM', 0, N'AQAAAAIAAYagAAAAENtwtraMdpz9ddKAyb1D35us/N/dmXXIAdmP8EmLwvB92Q3XulE5JdDAKv4FzYAXjg==', N'ACW2WEESM52XZRPQIIQILOVX3HR6B7AX', N'af1cd803-f32c-48d7-99f2-1afd6671a5d7', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'f9acd7a6-e786-4b65-b6b4-c2c96a4bc946', 0, 1, N'Inhaaa', N'INHAAA', N'Inha@hotmail.com', N'INHA@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEJayVl1JhWYvfzvOix9j0OYV6IHp67Ys0eQ+GXzvdwhlCwbufQzlGTLmXI8EmR8ODA==', N'EJKMFXQBH6Z24MBD6ZCHRYWZ75BZT5AR', N'3760c384-e2ad-4ce6-b9d9-e9b80c3ad082', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[AspNetUsers] ([Id], [IsAdmin], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Login]) VALUES (N'fc039b95-b150-4dfb-85c1-8d16048c6e97', 0, 1, N'Inhaa', N'INHAA', N'inha@gmail.om', N'INHA@GMAIL.OM', 1, N'AQAAAAIAAYagAAAAECDU8tZw+b0OY65Exvb/3PLwtFrejew3haCnQrtp20LeI62tK6jfl9m5Ujb4yo5YMA==', N'OZV5UG5PXTQ2YP3WUJMWKJXGOZ4U73TO', N'e97f1ab5-758a-45c4-8aad-87cf869be2c4', NULL, 0, 0, NULL, 1, 0, N'')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'10891d92-e700-4555-a648-4cce324b6b06', N'Romance', 1, 0, N'8e79c109-cfbd-4e6e-8f26-ad3fc1621d35')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'38305fd2-6128-4812-9c09-7b7df1f7f26f', N'Broderie', 1, 1, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'39c0b1f3-ebd3-4b5d-b2ba-c3254bb96f7d', N'Switch', 1, 0, N'e2726f61-4cf2-4228-ad7c-2f4d7655b944')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'41b5c2d8-f346-4ebd-9d93-8ec06e371ded', N'Aventure', 1, 0, N'8e79c109-cfbd-4e6e-8f26-ad3fc1621d35')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'52cf80a3-c5ca-4222-a7d5-8dae94725dfe', N'Roman', 1, 1, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'595dcdab-c146-42f6-b01c-3d6fff2ddcd7', N'PointCroix', 1, 0, N'38305fd2-6128-4812-9c09-7b7df1f7f26f')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'62750247-032e-4266-827d-07790ff0efd5', N'Shojo', 1, 0, N'6b7ab64d-041f-4a8e-9040-7a7320dff81e')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'69b59d54-e609-44e8-8b58-7843b8d8e307', N'Policier', 1, 0, N'52cf80a3-c5ca-4222-a7d5-8dae94725dfe')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'6b7ab64d-041f-4a8e-9040-7a7320dff81e', N'Manga', 1, 1, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'7b2f7954-2807-427c-8496-e8943d1d1854', N'Accessoire', 1, 0, N'cd0eff5f-5586-475d-9c0a-a2a202714820')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'7d37233f-fbbe-4eae-a7a8-1d2e6b177f73', N'Sashiko', 1, 0, N'38305fd2-6128-4812-9c09-7b7df1f7f26f')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'8152a12c-5c08-4c67-b9cf-0893a9419021', N'Sheinen', 1, 0, N'6b7ab64d-041f-4a8e-9040-7a7320dff81e')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'8e79c109-cfbd-4e6e-8f26-ad3fc1621d35', N'Webtoon', 1, 1, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'9a15409a-a179-471a-b877-309392724da7', N'societe', 1, 0, N'e2726f61-4cf2-4228-ad7c-2f4d7655b944')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'ad84aa9b-4e8e-4921-a616-9431ebfe1798', N'Steams', 1, 0, N'e2726f61-4cf2-4228-ad7c-2f4d7655b944')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'cd0eff5f-5586-475d-9c0a-a2a202714820', N'Couture', 1, 1, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'ddf846d5-2fc3-431a-b9e0-8341e09d7a69', N'Traditionnelle', 1, 0, N'38305fd2-6128-4812-9c09-7b7df1f7f26f')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'de503c3f-86d3-4493-a3d5-36940eca8a17', N'Vetement', 1, 0, N'cd0eff5f-5586-475d-9c0a-a2a202714820')
INSERT [dbo].[Categories] ([Id], [Name], [Is_active], [Is_principal], [Parent_id]) VALUES (N'e2726f61-4cf2-4228-ad7c-2f4d7655b944', N'Jeux', 1, 1, NULL)
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'62750247-032e-4266-827d-07790ff0efd5', N'33886d47-1721-4300-8bdd-bcf75bb771b8')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'6b7ab64d-041f-4a8e-9040-7a7320dff81e', N'33886d47-1721-4300-8bdd-bcf75bb771b8')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'38305fd2-6128-4812-9c09-7b7df1f7f26f', N'4619c4ec-31e2-4604-91d4-6cc5b7a2538e')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'ddf846d5-2fc3-431a-b9e0-8341e09d7a69', N'4619c4ec-31e2-4604-91d4-6cc5b7a2538e')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'10891d92-e700-4555-a648-4cce324b6b06', N'73ee3e0b-e869-4646-a778-3d4b06fd4ba9')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'8e79c109-cfbd-4e6e-8f26-ad3fc1621d35', N'73ee3e0b-e869-4646-a778-3d4b06fd4ba9')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'cd0eff5f-5586-475d-9c0a-a2a202714820', N'85f37621-aee7-4bba-96af-15088f33056a')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'62750247-032e-4266-827d-07790ff0efd5', N'94bd02b6-1401-4c85-a099-e7ead437faa8')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'6b7ab64d-041f-4a8e-9040-7a7320dff81e', N'94bd02b6-1401-4c85-a099-e7ead437faa8')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'62750247-032e-4266-827d-07790ff0efd5', N'9dd2c360-b66e-48bd-a387-885d4c6536c8')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'6b7ab64d-041f-4a8e-9040-7a7320dff81e', N'9dd2c360-b66e-48bd-a387-885d4c6536c8')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'38305fd2-6128-4812-9c09-7b7df1f7f26f', N'ae6d4b34-36a8-480e-a985-2d35b1224c6d')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'ddf846d5-2fc3-431a-b9e0-8341e09d7a69', N'ae6d4b34-36a8-480e-a985-2d35b1224c6d')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'6b7ab64d-041f-4a8e-9040-7a7320dff81e', N'b3b45d34-f4e7-4e23-8b02-9e09df6ae87a')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'62750247-032e-4266-827d-07790ff0efd5', N'c47928a3-b378-49d0-94df-6172d3d9cc58')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'6b7ab64d-041f-4a8e-9040-7a7320dff81e', N'c47928a3-b378-49d0-94df-6172d3d9cc58')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'6b7ab64d-041f-4a8e-9040-7a7320dff81e', N'e1da082a-5714-4a71-86da-b64099d5158f')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'8152a12c-5c08-4c67-b9cf-0893a9419021', N'e1da082a-5714-4a71-86da-b64099d5158f')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'62750247-032e-4266-827d-07790ff0efd5', N'e2373a6e-0e5c-4544-a53b-8e99a4fdfe0d')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'6b7ab64d-041f-4a8e-9040-7a7320dff81e', N'e2373a6e-0e5c-4544-a53b-8e99a4fdfe0d')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'10891d92-e700-4555-a648-4cce324b6b06', N'f3f3ab13-708c-44d5-bbe9-47d9f305b461')
INSERT [dbo].[Category_Articles] ([CategoryId], [ArticleId]) VALUES (N'8e79c109-cfbd-4e6e-8f26-ad3fc1621d35', N'f3f3ab13-708c-44d5-bbe9-47d9f305b461')
INSERT [dbo].[Collections] ([UserId], [ArticleId], [Note], [Statut], [Text], [Purchased_Price], [Purchased_Date]) VALUES (N'20d8619b-ab59-40c4-902f-bea6136cc0ca', N'33886d47-1721-4300-8bdd-bcf75bb771b8', 3, 0, NULL, 0, NULL)
INSERT [dbo].[Collections] ([UserId], [ArticleId], [Note], [Statut], [Text], [Purchased_Price], [Purchased_Date]) VALUES (N'20d8619b-ab59-40c4-902f-bea6136cc0ca', N'73ee3e0b-e869-4646-a778-3d4b06fd4ba9', NULL, 2, NULL, 15.1, CAST(N'2025-03-18T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Collections] ([UserId], [ArticleId], [Note], [Statut], [Text], [Purchased_Price], [Purchased_Date]) VALUES (N'20d8619b-ab59-40c4-902f-bea6136cc0ca', N'94bd02b6-1401-4c85-a099-e7ead437faa8', 4, 2, NULL, 0, NULL)
INSERT [dbo].[Collections] ([UserId], [ArticleId], [Note], [Statut], [Text], [Purchased_Price], [Purchased_Date]) VALUES (N'20d8619b-ab59-40c4-902f-bea6136cc0ca', N'b3b45d34-f4e7-4e23-8b02-9e09df6ae87a', 4, 0, NULL, 7.95, NULL)
INSERT [dbo].[Collections] ([UserId], [ArticleId], [Note], [Statut], [Text], [Purchased_Price], [Purchased_Date]) VALUES (N'20d8619b-ab59-40c4-902f-bea6136cc0ca', N'e1da082a-5714-4a71-86da-b64099d5158f', 5, 2, NULL, 0, NULL)
INSERT [dbo].[Collections] ([UserId], [ArticleId], [Note], [Statut], [Text], [Purchased_Price], [Purchased_Date]) VALUES (N'20d8619b-ab59-40c4-902f-bea6136cc0ca', N'f3f3ab13-708c-44d5-bbe9-47d9f305b461', NULL, 1, NULL, 0, NULL)
INSERT [dbo].[Families] ([Id], [Name], [Description]) VALUES (N'7d2f5f9c-3b87-4336-9126-23593b069470', N'Webtoon ', NULL)
INSERT [dbo].[Families] ([Id], [Name], [Description]) VALUES (N'82cb1d5c-99df-48e1-a5d9-1909325b55bc', N'Arina Tenemura', NULL)
INSERT [dbo].[Families] ([Id], [Name], [Description]) VALUES (N'8b9e375c-ca40-4b60-b464-1a4654bddaf3', N'Sakura', N'Pour les fans de Sakura ')
INSERT [dbo].[Family_Users] ([FamilyId], [UserId], [Is_manager]) VALUES (N'7d2f5f9c-3b87-4336-9126-23593b069470', N'1decba04-5a1c-44a5-ad31-249527dbf60a', 1)
INSERT [dbo].[Family_Users] ([FamilyId], [UserId], [Is_manager]) VALUES (N'82cb1d5c-99df-48e1-a5d9-1909325b55bc', N'20d8619b-ab59-40c4-902f-bea6136cc0ca', 1)
INSERT [dbo].[Family_Users] ([FamilyId], [UserId], [Is_manager]) VALUES (N'8b9e375c-ca40-4b60-b464-1a4654bddaf3', N'20d8619b-ab59-40c4-902f-bea6136cc0ca', 1)
SET IDENTITY_INSERT [dbo].[Images] ON 

INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2023, N'.jpg', N'/images/My-happy-marriage-Tome-1.jpg', N'e2373a6e-0e5c-4544-a53b-8e99a4fdfe0d')
INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2024, N'.jpg', N'/images/My-happy-marriage-Tome-2.jpg', N'9dd2c360-b66e-48bd-a387-885d4c6536c8')
INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2025, N'.jpg', N'/images/a.jpg', N'85f37621-aee7-4bba-96af-15088f33056a')
INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2026, N'.jpg', N'/images/aa.jpg', N'4619c4ec-31e2-4604-91d4-6cc5b7a2538e')
INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2027, N'.jpg', N'/images/aaa.jpg', N'ae6d4b34-36a8-480e-a985-2d35b1224c6d')
INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2028, N'.jpg', N'/images/b.jpg', N'c47928a3-b378-49d0-94df-6172d3d9cc58')
INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2029, N'.jpg', N'/images/cover_f22dfd88-c6d5-4a40-96d3-44166a2dc7d5.jpg', N'e1da082a-5714-4a71-86da-b64099d5158f')
INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2030, N'.jpg', N'/images/cover_dd716ffb-c99b-4ac1-99d1-a89fff030602.jpg', N'94bd02b6-1401-4c85-a099-e7ead437faa8')
INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2031, N'.jpg', N'/images/Bibliophile-Prince-T01.jpg', N'33886d47-1721-4300-8bdd-bcf75bb771b8')
INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2032, N'.jpg', N'/images/Untouchable-Lady-T01.jpg', N'73ee3e0b-e869-4646-a778-3d4b06fd4ba9')
INSERT [dbo].[Images] ([Id], [Type], [Root], [ArticleId]) VALUES (2033, N'.jpg', N'/images/cover_c4fded99-b9a1-4ef7-a11d-a8c928e363fd.jpg', N'f3f3ab13-708c-44d5-bbe9-47d9f305b461')
SET IDENTITY_INSERT [dbo].[Images] OFF
SET IDENTITY_INSERT [dbo].[MemberShip_Families] ON 

INSERT [dbo].[MemberShip_Families] ([Id], [RequesterId], [FamilyId], [Statut]) VALUES (5087, N'1decba04-5a1c-44a5-ad31-249527dbf60a', N'8b9e375c-ca40-4b60-b464-1a4654bddaf3', 0)
SET IDENTITY_INSERT [dbo].[MemberShip_Families] OFF
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([Id], [Text], [Date], [Statut], [UserId], [MessageObjet]) VALUES (1033, N'Bonjour, peux on avoir une catégorie jeux ?', CAST(N'2025-03-25T10:10:42.2265623' AS DateTime2), 1, N'20d8619b-ab59-40c4-902f-bea6136cc0ca', N'Catégorie Jeux')
SET IDENTITY_INSERT [dbo].[Messages] OFF
SET IDENTITY_INSERT [dbo].[Notepads] ON 

INSERT [dbo].[Notepads] ([Id], [Text], [Date_reminder], [UserId], [Titre], [IsEmailSent]) VALUES (3015, N'Date de sortie 17 avril', CAST(N'2025-04-17T00:00:00.0000000' AS DateTime2), N'20d8619b-ab59-40c4-902f-bea6136cc0ca', N'Autrice de ma destiné 3', 0)
SET IDENTITY_INSERT [dbo].[Notepads] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Articles_Isbn]    Script Date: 25-03-25 10:35:30 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Articles_Isbn] ON [dbo].[Articles]
(
	[Isbn] ASC
)
WHERE ([Isbn] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Articles_Name]    Script Date: 25-03-25 10:35:30 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Articles_Name] ON [dbo].[Articles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 25-03-25 10:35:30 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUsers_Email]    Script Date: 25-03-25 10:35:30 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AspNetUsers_Email] ON [dbo].[AspNetUsers]
(
	[Email] ASC
)
WHERE ([Email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUsers_UserName]    Script Date: 25-03-25 10:35:30 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AspNetUsers_UserName] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)
WHERE ([UserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 25-03-25 10:35:30 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Categories_Parent_id]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_Categories_Parent_id] ON [dbo].[Categories]
(
	[Parent_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Category_Articles_ArticleId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_Category_Articles_ArticleId] ON [dbo].[Category_Articles]
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Collections_ArticleId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_Collections_ArticleId] ON [dbo].[Collections]
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Families_Name]    Script Date: 25-03-25 10:35:30 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Families_Name] ON [dbo].[Families]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Family_Users_UserId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_Family_Users_UserId] ON [dbo].[Family_Users]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Images_ArticleId]    Script Date: 25-03-25 10:35:30 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Images_ArticleId] ON [dbo].[Images]
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_MemberShip_Families_FamilyId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_MemberShip_Families_FamilyId] ON [dbo].[MemberShip_Families]
(
	[FamilyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_MemberShip_Families_RequesterId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_MemberShip_Families_RequesterId] ON [dbo].[MemberShip_Families]
(
	[RequesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Messages_UserId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_Messages_UserId] ON [dbo].[Messages]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Notepads_UserId]    Script Date: 25-03-25 10:35:30 ******/
CREATE NONCLUSTERED INDEX [IX_Notepads_UserId] ON [dbo].[Notepads]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [Login]
GO
ALTER TABLE [dbo].[Family_Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Is_manager]
GO
ALTER TABLE [dbo].[Messages] ADD  DEFAULT (N'') FOR [MessageObjet]
GO
ALTER TABLE [dbo].[Notepads] ADD  DEFAULT (N'') FOR [Titre]
GO
ALTER TABLE [dbo].[Notepads] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsEmailSent]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Categories_Parent_id] FOREIGN KEY([Parent_id])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Categories_Parent_id]
GO
ALTER TABLE [dbo].[Category_Articles]  WITH CHECK ADD  CONSTRAINT [FK_Category_Articles_Articles_ArticleId] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Category_Articles] CHECK CONSTRAINT [FK_Category_Articles_Articles_ArticleId]
GO
ALTER TABLE [dbo].[Category_Articles]  WITH CHECK ADD  CONSTRAINT [FK_Category_Articles_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Category_Articles] CHECK CONSTRAINT [FK_Category_Articles_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Collections]  WITH CHECK ADD  CONSTRAINT [FK_Collections_Articles_ArticleId] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Collections] CHECK CONSTRAINT [FK_Collections_Articles_ArticleId]
GO
ALTER TABLE [dbo].[Collections]  WITH CHECK ADD  CONSTRAINT [FK_Collections_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Collections] CHECK CONSTRAINT [FK_Collections_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Family_Users]  WITH CHECK ADD  CONSTRAINT [FK_Family_Users_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Family_Users] CHECK CONSTRAINT [FK_Family_Users_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Family_Users]  WITH CHECK ADD  CONSTRAINT [FK_Family_Users_Families_FamilyId] FOREIGN KEY([FamilyId])
REFERENCES [dbo].[Families] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Family_Users] CHECK CONSTRAINT [FK_Family_Users_Families_FamilyId]
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_Articles_ArticleId] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_Articles_ArticleId]
GO
ALTER TABLE [dbo].[MemberShip_Families]  WITH CHECK ADD  CONSTRAINT [FK_MemberShip_Families_AspNetUsers_RequesterId] FOREIGN KEY([RequesterId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[MemberShip_Families] CHECK CONSTRAINT [FK_MemberShip_Families_AspNetUsers_RequesterId]
GO
ALTER TABLE [dbo].[MemberShip_Families]  WITH CHECK ADD  CONSTRAINT [FK_MemberShip_Families_Families_FamilyId] FOREIGN KEY([FamilyId])
REFERENCES [dbo].[Families] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MemberShip_Families] CHECK CONSTRAINT [FK_MemberShip_Families_Families_FamilyId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Notepads]  WITH CHECK ADD  CONSTRAINT [FK_Notepads_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notepads] CHECK CONSTRAINT [FK_Notepads_AspNetUsers_UserId]
GO
USE [master]
GO
ALTER DATABASE [GestionnaireCollection] SET  READ_WRITE 
GO
