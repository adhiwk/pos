USE [eretail]
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_access_control_rule]    Script Date: 02/04/2021 15:06:40 ******/
CREATE TYPE [dbo].[TVP_access_control_rule] AS TABLE(
	[user id] [nchar](15) NULL,
	[ac_id] [int] NULL,
	[access] [nchar](1) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_Barang_Harga_Bertingkats]    Script Date: 02/04/2021 15:06:40 ******/
CREATE TYPE [dbo].[TVP_Barang_Harga_Bertingkats] AS TABLE(
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[qty min] [decimal](18, 2) NULL,
	[qty max] [decimal](18, 2) NULL,
	[harga jual] [decimal](18, 2) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_Barang_Stoks]    Script Date: 02/04/2021 15:06:40 ******/
CREATE TYPE [dbo].[TVP_Barang_Stoks] AS TABLE(
	[kode barang] [nchar](25) NULL,
	[plu] [nchar](15) NULL,
	[kode gudang] [nchar](3) NULL,
	[stok awal] [decimal](18, 2) NULL,
	[tgl stok] [date] NULL,
	[user id] [nchar](15) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_Barang_Sub_Item]    Script Date: 02/04/2021 15:06:40 ******/
CREATE TYPE [dbo].[TVP_Barang_Sub_Item] AS TABLE(
	[kode barang] [nchar](25) NULL,
	[plu] [nchar](15) NULL,
	[nama barang] [nchar](100) NULL,
	[satuan terbesar] [nchar](50) NULL,
	[isi] [decimal](18, 2) NULL,
	[satuan terkecil] [nchar](50) NULL,
	[harga pokok pembelian] [decimal](18, 2) NULL,
	[ppn masuk] [decimal](18, 2) NULL,
	[harga pokok penjualan] [decimal](18, 2) NULL,
	[harga jual toko] [decimal](18, 2) NULL,
	[harga jual grosir] [decimal](18, 2) NULL,
	[sub item] [nchar](1) NULL,
	[sub plu] [nchar](15) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_pembayaran_piutang]    Script Date: 02/04/2021 15:06:40 ******/
CREATE TYPE [dbo].[TVP_pembayaran_piutang] AS TABLE(
	[kode bayar] [nchar](15) NULL,
	[kode cust] [nchar](5) NULL,
	[nomor nota] [nchar](15) NULL,
	[tgl] [datetime] NULL,
	[bayar] [decimal](18, 0) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_Pembelian_Details]    Script Date: 02/04/2021 15:06:40 ******/
CREATE TYPE [dbo].[TVP_Pembelian_Details] AS TABLE(
	[nomor faktur] [nchar](15) NULL,
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[qty] [decimal](18, 2) NULL,
	[satuan] [nchar](50) NULL,
	[harga pokok pembelian] [decimal](18, 2) NULL,
	[disc] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[sub total] [decimal](18, 2) NULL,
	[pajak] [nchar](1) NULL,
	[besar pajak] [decimal](18, 2) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_Penjualan_Detail_]    Script Date: 02/04/2021 15:06:40 ******/
CREATE TYPE [dbo].[TVP_Penjualan_Detail_] AS TABLE(
	[nomor nota] [nchar](15) NULL,
	[tgl nota] [datetime] NULL,
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[paket] [nchar](1) NULL,
	[grosir] [nchar](1) NULL,
	[qty] [decimal](18, 2) NULL,
	[kemasan] [nchar](15) NULL,
	[satuan] [nchar](50) NULL,
	[harga jual] [decimal](18, 2) NULL,
	[disc] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[sub total] [decimal](18, 2) NULL,
	[kode kasir] [char](5) NULL,
	[sub plu] [char](15) NULL,
	[isi] [decimal](18, 2) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_Penjualan_Details]    Script Date: 02/04/2021 15:06:40 ******/
CREATE TYPE [dbo].[TVP_Penjualan_Details] AS TABLE(
	[nomor nota] [nchar](15) NULL,
	[tgl nota] [datetime] NULL,
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[paket] [nchar](1) NULL,
	[grosir] [nchar](1) NULL,
	[qty] [decimal](18, 2) NULL,
	[kemasan] [nchar](15) NULL,
	[satuan] [nchar](50) NULL,
	[harga jual] [decimal](18, 2) NULL,
	[disc] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[sub total] [decimal](18, 2) NULL,
	[kode kasir] [char](5) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_Penjualan_Tampung_Detail]    Script Date: 02/04/2021 15:06:40 ******/
CREATE TYPE [dbo].[TVP_Penjualan_Tampung_Detail] AS TABLE(
	[nomor tampung] [nchar](25) NULL,
	[nama barang] [nchar](50) NULL,
	[kode barang] [nchar](25) NULL,
	[paket] [nchar](1) NULL,
	[grosir] [nchar](1) NULL,
	[qty] [decimal](18, 2) NULL,
	[kemasan] [nchar](15) NULL,
	[satuan] [nchar](50) NULL,
	[harga jual] [decimal](18, 2) NULL,
	[disc] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[sub total] [decimal](18, 2) NULL,
	[plu] [nchar](15) NULL
)
GO
/****** Object:  Table [dbo].[access_control]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[access_control](
	[parent] [nchar](50) NULL,
	[process] [nchar](50) NULL,
	[ac_id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[access_control_rule]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[access_control_rule](
	[user id] [nchar](15) NULL,
	[ac_id] [int] NULL,
	[access] [nchar](1) NULL CONSTRAINT [DF_access_control_rule_access]  DEFAULT ('T'),
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[barang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[barang](
	[kode barang] [nchar](25) NULL,
	[plu] [nchar](15) NULL,
	[nama barang] [char](100) NULL,
	[kode kategori] [nchar](5) NULL,
	[kode sub kategori] [nchar](10) NULL,
	[kode produsen] [nchar](5) NULL,
	[kode suplier] [nchar](5) NULL,
	[bentuk sediaan] [nchar](50) NULL,
	[satuan terbesar] [nchar](50) NULL,
	[isi] [decimal](18, 0) NULL,
	[satuan terkecil] [nchar](50) NULL,
	[harga pokok pembelian] [decimal](18, 2) NULL,
	[ppn masuk] [decimal](18, 2) NULL,
	[harga pokok penjualan] [decimal](18, 2) NULL,
	[harga jual toko] [decimal](18, 2) NULL,
	[harga jual grosir] [decimal](18, 2) NULL,
	[kena pajak] [nchar](1) NULL,
	[stok minimum] [decimal](18, 2) NULL,
	[stok] [decimal](18, 2) NULL,
	[sub item] [nchar](1) NULL,
	[sub plu] [nchar](15) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[barang_gudang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[barang_gudang](
	[kode gudang] [nchar](3) NULL,
	[nama gudang] [nchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[barang_harga_bertingkat]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[barang_harga_bertingkat](
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[qty min] [decimal](18, 2) NULL,
	[qty max] [decimal](18, 2) NULL,
	[harga jual] [decimal](18, 2) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[barang_laporan_stok]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[barang_laporan_stok](
	[plu] [nchar](15) NULL,
	[harga pokok pembelian] [decimal](18, 2) NULL,
	[jumlah stok] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[barang_laporan_stok_minimum]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[barang_laporan_stok_minimum](
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[harga pokok pembelian] [decimal](18, 2) NULL,
	[stok minimum] [decimal](18, 2) NULL,
	[jumlah stok] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[barang_mutasi]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[barang_mutasi](
	[nomor mutasi] [nchar](15) NULL,
	[kode gudang] [nchar](3) NULL,
	[tgl mutasi] [date] NULL,
	[jenis mutasi] [nchar](3) NULL,
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[qty] [decimal](18, 2) NULL,
	[satuan] [nchar](50) NULL,
	[user id] [nchar](15) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[barang_pengaturan_markup]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[barang_pengaturan_markup](
	[persen harga toko] [int] NULL,
	[persen harga grosir] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[barang_stok_awal]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[barang_stok_awal](
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[kode gudang] [nchar](3) NULL,
	[stok awal] [decimal](18, 2) NULL,
	[tgl stok] [date] NULL,
	[user id] [nchar](15) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[customer]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[kode cust] [nchar](10) NULL,
	[nama] [nchar](50) NULL,
	[alamat] [nchar](50) NULL,
	[no hp] [nchar](15) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[dokter]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dokter](
	[kode dokter] [nchar](5) NULL,
	[nama dokter] [nchar](50) NULL,
	[spesialisasi] [nchar](30) NULL,
	[no hp] [nchar](15) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hutang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hutang](
	[kode suplier] [nchar](5) NULL,
	[nomor faktur] [nchar](15) NULL,
	[tgl faktur] [date] NULL,
	[tgl jatuh tempo] [date] NULL,
	[saldo terhutang] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[kategori]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kategori](
	[kode kategori] [nchar](5) NULL,
	[nama kategori] [nchar](50) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pembayaran_piutang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pembayaran_piutang](
	[kode bayar] [nchar](15) NULL,
	[kode cust] [nchar](5) NULL,
	[nomor nota] [nchar](15) NULL,
	[tgl] [datetime] NULL,
	[bayar] [decimal](18, 0) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pembelian]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pembelian](
	[nomor faktur] [nchar](15) NULL,
	[kode suplier] [nchar](5) NULL,
	[tgl faktur] [date] NULL,
	[jenis] [nchar](1) NULL,
	[tgl terima] [date] NULL,
	[tgl jatuh tempo] [date] NULL,
	[biaya lain] [decimal](18, 2) NULL,
	[total pajak] [decimal](18, 2) NULL,
	[uang muka] [decimal](18, 2) NULL,
	[saldo terhutang] [decimal](18, 2) NULL,
	[user id] [nchar](15) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pembelian_detail]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pembelian_detail](
	[nomor faktur] [nchar](15) NULL,
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[qty] [decimal](18, 2) NULL,
	[satuan] [nchar](50) NULL,
	[harga pokok pembelian] [decimal](18, 2) NULL,
	[disc] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[sub total] [decimal](18, 2) NULL,
	[pajak] [nchar](1) NULL,
	[besar pajak] [decimal](18, 2) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pembelian_log]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pembelian_log](
	[nomor faktur] [nchar](15) NULL,
	[tgl] [datetime] NULL,
	[user id] [nchar](15) NULL,
	[reason] [nchar](15) NULL,
	[plu] [nchar](15) NULL,
	[harga pokok pembelian] [decimal](18, 2) NULL,
	[qty] [decimal](18, 2) NULL,
	[qty_to] [decimal](18, 2) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pembelian_qty]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pembelian_qty](
	[nomor faktur] [nchar](15) NULL,
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[qty] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pembelian_rekap]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pembelian_rekap](
	[nomor faktur] [nchar](15) NULL,
	[tgl faktur] [date] NULL,
	[kode suplier] [nchar](5) NULL,
	[sub total] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[total pajak] [decimal](18, 2) NULL,
	[terbayar] [decimal](18, 2) NULL,
	[sisa hutang] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pengaturan]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pengaturan](
	[nama toko] [nchar](38) NULL,
	[alamat toko] [nchar](38) NULL,
	[kota] [nchar](38) NULL,
	[npwp] [nchar](38) NULL,
	[no telpon] [nchar](38) NULL,
	[store number] [nchar](5) NULL,
	[store name] [nchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pengeluaran]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pengeluaran](
	[user id] [nchar](15) NULL,
	[kode kasir] [nchar](5) NULL,
	[shift] [nchar](10) NULL,
	[tgl transaksi] [date] NULL,
	[pengeluaran] [decimal](18, 3) NULL,
	[keterangan] [text] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[penjualan]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[penjualan](
	[nomor nota] [nchar](15) NULL,
	[tgl nota] [datetime] NULL,
	[tunai] [nchar](1) NULL,
	[jatuh tempo] [date] NULL,
	[paket] [nchar](1) NULL,
	[grosir] [nchar](1) NULL,
	[kode sales] [nchar](10) NULL,
	[kode cust] [nchar](10) NULL,
	[user id] [nchar](15) NULL,
	[cara bayar] [nchar](11) NULL,
	[jenis kartu] [nchar](20) NULL,
	[appr code] [nchar](15) NULL,
	[nomor kartu] [nchar](20) NULL,
	[tanggal expired] [nchar](5) NULL,
	[shift] [nchar](1) NULL,
	[total] [decimal](18, 2) NULL,
	[pembulatan] [decimal](18, 2) NULL,
	[bayar] [decimal](18, 2) NULL,
	[saldo terhutang] [decimal](18, 2) NULL,
	[kembali] [decimal](18, 2) NULL,
	[kode kasir] [nchar](3) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[penjualan_detail]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[penjualan_detail](
	[nomor nota] [nchar](15) NULL,
	[tgl nota] [datetime] NULL,
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[paket] [nchar](1) NULL,
	[grosir] [nchar](1) NULL,
	[qty] [decimal](18, 2) NULL,
	[kemasan] [nchar](15) NULL,
	[satuan] [nchar](50) NULL,
	[harga jual] [decimal](18, 2) NULL,
	[disc] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[sub total] [decimal](18, 2) NULL,
	[kode kasir] [char](5) NULL,
	[sub plu] [nchar](15) NULL,
	[isi] [decimal](18, 2) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[penjualan_log]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[penjualan_log](
	[nomor nota] [nchar](15) NULL,
	[tgl] [datetime] NULL,
	[user id] [nchar](15) NULL,
	[reason] [nchar](15) NULL,
	[plu] [nchar](15) NULL,
	[harga jual] [decimal](18, 2) NULL,
	[qty] [decimal](18, 2) NULL,
	[qty_to] [decimal](18, 2) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[penjualan_qty]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[penjualan_qty](
	[nomor nota] [nchar](15) NULL,
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[qty] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[penjualan_retur]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[penjualan_retur](
	[nomor resep] [nchar](15) NULL,
	[tgl retur] [date] NULL,
	[user id] [nchar](15) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[penjualan_retur_detail]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[penjualan_retur_detail](
	[nomor nota] [nchar](15) NULL,
	[plu] [nchar](15) NULL,
	[kode barang] [nchar](25) NULL,
	[qty] [decimal](18, 2) NULL,
	[harga jual] [decimal](18, 2) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[penjualan_tampung]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[penjualan_tampung](
	[nomor tampung] [nchar](25) NULL,
	[tgl nota] [date] NULL,
	[tunai] [nchar](1) NULL,
	[paket] [nchar](1) NULL,
	[grosir] [nchar](1) NULL,
	[kode cust] [nchar](5) NULL,
	[kode sales] [nchar](5) NULL,
	[cust name] [nchar](50) NULL,
	[user id] [nchar](15) NULL,
	[kode kasir] [nchar](3) NULL,
	[shift] [nchar](1) NULL,
	[status] [nchar](1) NULL CONSTRAINT [DF_penjualan_tampung_status]  DEFAULT (N'T')
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[penjualan_tampung_detail]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[penjualan_tampung_detail](
	[nomor tampung] [nchar](25) NULL,
	[nama barang] [nchar](50) NULL,
	[kode barang] [nchar](25) NULL,
	[paket] [nchar](1) NULL,
	[grosir] [nchar](1) NULL,
	[qty] [decimal](18, 2) NULL,
	[kemasan] [nchar](15) NULL,
	[satuan] [nchar](50) NULL,
	[harga jual] [decimal](18, 2) NULL,
	[disc] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[sub total] [decimal](18, 2) NULL,
	[plu] [nchar](15) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[piutang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[piutang](
	[kode cust] [nchar](5) NULL,
	[nomor nota] [nchar](15) NULL,
	[tgl nota] [datetime] NULL,
	[jatuh tempo] [date] NULL,
	[piutang] [decimal](18, 0) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[produsen]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[produsen](
	[kode produsen] [nchar](5) NULL,
	[nama produsen] [nchar](50) NULL,
	[alamat] [nchar](50) NULL,
	[npwp] [nchar](50) NULL,
	[email kontak] [nchar](50) NULL,
	[telpon kontak] [nchar](50) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[rekap_kasir]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rekap_kasir](
	[user id] [nchar](15) NULL,
	[tgl transaksi] [date] NULL,
	[kode kasir] [nchar](3) NULL,
	[shift] [nchar](10) NULL,
	[modal] [decimal](18, 3) NULL,
	[kas aktual] [decimal](18, 3) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sales]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sales](
	[kode sales] [nchar](5) NULL,
	[nama] [nchar](50) NULL,
	[alamat] [nchar](50) NULL,
	[no hp] [nchar](15) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[shift_kerja]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shift_kerja](
	[shift] [nchar](1) NULL,
	[waktu] [nchar](30) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sub_kategori]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sub_kategori](
	[kode kategori] [nchar](5) NULL,
	[kode sub kategori] [nchar](10) NULL,
	[nama sub kategori] [nchar](50) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[suplier]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[suplier](
	[kode suplier] [nchar](5) NULL,
	[nama suplier] [nchar](50) NULL,
	[alamat] [nchar](50) NULL,
	[suplier kena pajak] [nchar](1) NULL,
	[npwp] [nchar](50) NULL,
	[email kontak] [nchar](50) NULL,
	[phone kontak] [nchar](50) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tabel_bantuan]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tabel_bantuan](
	[jenis data] [nchar](50) NULL,
	[keterangan] [nchar](50) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[nama user] [nchar](20) NULL,
	[user id] [nchar](15) NULL,
	[password] [char](32) NULL,
	[nomor hp] [nchar](15) NULL,
	[grup id] [nchar](20) NULL,
	[item_add] [nchar](1) NULL,
	[item_update] [nchar](1) NULL,
	[item_delete] [nchar](1) NULL,
	[item_view] [nchar](1) NULL,
	[item_stock_report] [nchar](1) NULL,
	[item_min_stock_report] [nchar](1) NULL,
	[penjualan_add] [nchar](1) NULL,
	[penjualan_edit_item] [nchar](1) NULL,
	[penjualan_cancel_item] [nchar](1) NULL,
	[penjualan_koreksi] [nchar](1) NULL,
	[penjualan_update] [nchar](1) NULL,
	[penjualan_delete] [nchar](1) NULL,
	[penjualan_view] [nchar](1) NULL,
	[penjualan_report] [nchar](1) NULL,
	[pembelian_add] [nchar](1) NULL,
	[pembelian_koreksi] [nchar](1) NULL,
	[pembelian_update] [nchar](1) NULL,
	[pembelian_delete] [nchar](1) NULL,
	[pembelian_view] [nchar](1) NULL,
	[pembelian_report] [nchar](1) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[add_barang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_barang]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_barang nchar(25),
   @plu nchar(15),
   @nama_barang nchar(50),
   @kode_kategori nchar(5),
   @kode_sub_kategori nchar(10),
   @kode_produsen nchar(5),
   @kode_suplier nchar(5),
   @bentuk_sediaan nchar(50),
   @satuan_terbesar nchar(50),
   @isi decimal(18,2),
   @satuan_terkecil nchar(50),
   @harga_pokok_pembelian decimal(18,2),
   @ppn_masuk decimal(18,2),
   @harga_pokok_penjualan decimal(18,2),
   @harga_jual_toko decimal(18,2),
   @harga_jual_grosir decimal(18,2),
   @kena_pajak nchar(1),
   @stok_minimum decimal(18,2),
   @sub_item nchar(1),
   @Barang_Stok_TVP TVP_Barang_Stoks ReadOnly,
   @Barang_Sub_Item_TVP TVP_Barang_Sub_Item ReadOnly    
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert data obat
    insert into barang ([kode barang],
	                  [plu],
	                  [nama barang],
					  [kode kategori],
					  [kode sub kategori], 
                      [kode produsen],
					  [kode suplier], 
                      [bentuk sediaan],
					  [satuan terbesar],
                      [isi],
					  [satuan terkecil],
					  [harga pokok pembelian],
					  [ppn masuk],
                      [harga pokok penjualan],
					  [harga jual toko],
					  [harga jual grosir],
					  [kena pajak],
					  [stok minimum],
					  [sub item])
    values (@kode_barang,
	        @plu,
	        @nama_barang,
			@kode_kategori,
			@kode_sub_kategori,
			@kode_produsen,
            @kode_suplier,
			@bentuk_sediaan,
			@satuan_terbesar,
			@isi,
            @satuan_terkecil,
			@harga_pokok_pembelian,
			@ppn_masuk,
			@harga_pokok_penjualan,
            @harga_jual_toko,
			@harga_jual_grosir,
			@kena_pajak,
			@stok_minimum,
			@sub_item)      
         
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data obat'
        return (1)
      End
  
   -- insert data obat_stok
    insert into barang_stok_awal (
	[kode barang],
	[plu],
	[kode gudang],
	[stok awal],
	[tgl stok],
	[user id])
    select [kode barang],
	[plu],
	[kode gudang],
	[stok awal],
	[tgl stok],
	[user id] from @Barang_Stok_TVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data ijin'
        return (1)
      End             
           
    insert into barang ([kode barang],
	                  [plu],
	                  [nama barang],
					  [satuan terbesar],
                      [isi],
					  [satuan terkecil],
					  [harga pokok pembelian],
					  [ppn masuk],
                      [harga pokok penjualan],
					  [harga jual toko],
					  [harga jual grosir],
					  [sub item],
					  [sub plu])
    select [kode barang],
	                  [plu],
	                  [nama barang],
					  [satuan terbesar],
                      [isi],
					  [satuan terkecil],
					  [harga pokok pembelian],
					  [ppn masuk],
                      [harga pokok penjualan],
					  [harga jual toko],
					  [harga jual grosir],
					  [sub item],
					  [sub plu]
    from @barang_sub_item_TVP

	set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data ijin'
        return (1)
      End    


  commit transaction
 return (0)
 end








GO
/****** Object:  StoredProcedure [dbo].[add_customer]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_customer]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_cust nchar(10),
   @nama nchar(50),
   @alamat nchar(50),
   @no_hp nchar(15)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into customer ([kode cust],
               [nama],
               [alamat],
			   [no hp])
   Values (@kode_cust,
           @nama,
		   @alamat,
           @no_hp)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_gudang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_gudang]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_gudang nchar(3),@nama_gudang nchar(50)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into barang_gudang ([kode gudang],[nama gudang])
   Values (@kode_gudang,@nama_gudang)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data gudang'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_harga_bertingkat]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_harga_bertingkat]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @HargaBarangBertingkatTVP TVP_Barang_Harga_Bertingkats ReadOnly
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
  
    -- insert data barang
   insert into barang_harga_bertingkat([plu],[kode barang],[qty min],[qty max],[harga jual])
   select [plu],[kode barang],[qty min],[qty max],[harga jual] from @HargaBarangBertingkatTVP
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data tampung penjualan'
        return (1)
      End
         
  commit transaction
 return (0)
 end



GO
/****** Object:  StoredProcedure [dbo].[add_kategori]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_kategori]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_kategori nchar(5),@nama_kategori nchar(50)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into kategori ([kode kategori],[nama kategori])
   Values (@kode_kategori,@nama_kategori)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_log_pembelian]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_log_pembelian]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @nomor_faktur nchar(15),
   @user_id nchar(15),
   @reason nchar(15),
   @plu nchar(15),
   @harga_pokok_pembelian decimal(18,2),
   @qty decimal(18,2),
   @qty_to decimal(18,2)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into pembelian_log ([nomor faktur],
               [tgl],
			   [user id],         
               [reason],
			   [plu],
			   [harga pokok pembelian],                              
               [qty],
			   [qty_to])
   values (@nomor_faktur,
           GETDATE(),
		   @user_id,
		   @reason,
		   @plu,
		   @harga_pokok_pembelian,
		   @qty,
		   @qty_to)                                 
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data log penjualan'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_log_penjualan]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_log_penjualan]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @nomor_nota nchar(15),
   @user_id nchar(15),
   @reason nchar(15),
   @plu nchar(15),
   @harga_jual decimal(18,2),
   @qty decimal(18,2),
   @qty_to decimal(18,2)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into penjualan_log ([nomor nota],
                              [tgl],
							  [user id],         
                              [reason],
							  [plu],
							  [harga jual],                              
                              [qty],
							  [qty_to])
   values (@nomor_nota,
           GETDATE(),
		   @user_id,
		   @reason,
		   @plu,
		   @harga_jual,
		   @qty,
		   @qty_to)                                 
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data log penjualan'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_pembayaran_piutang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[add_pembayaran_piutang]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @pembayaran_piutang_TVP TVP_pembayaran_piutang ReadOnly     
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
   
    insert into pembayaran_piutang([kode bayar],
	            [kode cust],
				[nomor nota],
				[tgl],
				[bayar])
    select [kode bayar],
	       [kode cust],
		   [nomor nota],
		   [tgl],
		   [bayar] from @pembayaran_piutang_TVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pembayaran data piutang'
        return (1)
      End             
    
	  commit transaction
 return (0)
 end








GO
/****** Object:  StoredProcedure [dbo].[add_pembelian]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_pembelian]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @nomor_faktur nchar(15),
   @kode_suplier nchar(5),
   @tgl_faktur nchar(10),
   @jenis nchar(1),
   @tgl_terima nchar(10),
   @tgl_jatuh_tempo nchar(10),
   @biaya_lain decimal(18,2),
   @total_pajak decimal(18,2),
   @uang_muka decimal(18,2),
   @saldo_terhutang decimal(18,2),
   @user_id nchar(15),
   @PembelianDetailTVP TVP_Pembelian_Details ReadOnly    
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert data obat
    insert into pembelian ([nomor faktur],[kode suplier], 
                           [tgl faktur],[jenis], [tgl terima], 
                           [tgl jatuh tempo],[biaya lain],
                           [total pajak],[uang muka],[saldo terhutang],[user id])
    values (@nomor_faktur,@kode_suplier,@tgl_faktur,@jenis,@tgl_terima, 
            @tgl_jatuh_tempo,@biaya_lain,
            @total_pajak,@uang_muka,@saldo_terhutang,@user_id)      
         
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data pembelian obat'
        return (1)
      End
  
   -- insert data obat_stok
    insert into pembelian_detail ([nomor faktur],[plu],[kode barang],[qty],[satuan],
                                [harga pokok pembelian],[disc],[discount],
								[sub total],[pajak],[besar pajak])
    select [nomor faktur],[plu],[kode barang],[qty],[satuan],
	       [harga pokok pembelian],[disc],[discount],[sub total],
		   [pajak],[besar pajak] from @PembelianDetailTVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data pembelian detail'
        return (1)
      End             
    
	   -- insert data obat_stok
    insert into pembelian_qty ([nomor faktur],[plu],[kode barang],[qty])
    select [nomor faktur],[plu],[kode barang],[qty] from @PembelianDetailTVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data pembelian qty'
        return (1)
      End
	
	if @jenis = 'K'
	 begin
	   insert into hutang ([kode suplier],[nomor faktur],[tgl faktur],[tgl jatuh tempo],[saldo terhutang])
       values (@kode_suplier,@nomor_faktur,@tgl_faktur,@tgl_jatuh_tempo,@saldo_terhutang)
    
       set @mERROR_NO = @@ERROR
       if @mERROR_NO <> 0
         begin
           Rollback transaction
           set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data hutang'
           return (1)
         end                    
	 end                      
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_pembelian_rekap]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[add_pembelian_rekap]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @nomor_faktur nchar(15),@tgl_faktur nchar(10),
   @kode_suplier nchar(5),@sub_total decimal(18,2),
   @discount decimal(18,2), @total_pajak decimal(18,2), 
   @terbayar decimal(18,2), @sisa_hutang decimal(18,2)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into pembelian_rekap ([nomor faktur],[tgl faktur], 
                                [kode suplier],[sub total],[discount],[total pajak],
                                [terbayar],[sisa hutang])
   values (@nomor_faktur,@tgl_faktur,@kode_suplier,@sub_total,@discount,@total_pajak,@terbayar,@sisa_hutang)                                 
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data pembelian rekap'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_penjualan]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[add_penjualan]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @nomor_nota nchar(15),
   @tgl_nota nchar(20),
   @kode_sales nchar(10),
   @kode_cust nchar(10),
   @tunai nchar(1),
   @tgl_jatuh_tempo nchar(10),
   @paket nchar(1),
   @grosir nchar(1),
   @user_id nchar(15),
   @cara_bayar nchar(11),
   @jenis_kartu nchar(20),
   @appr_code nchar(15),
   @nomor_kartu nchar(20),
   @tanggal_expired nchar(5),
   @kode_kasir nchar(5),
   @shift nchar(10),
   @PenjualanDetailTVP TVP_Penjualan_Detail_ ReadOnly,
   @total decimal(18,2),
   @pembulatan decimal(18,2),
   @bayar decimal(18,2),
   @saldo_terhutang decimal(18,2),
   @kembali decimal(18,2)    
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert data obat
    insert into penjualan ([nomor nota],
	                       [tgl nota],
						   [tunai],
						   [jatuh tempo],
						   [paket],
						   [grosir],
						   [kode sales],
						   [kode cust],
                           [user id],
						   [cara bayar],
						   [jenis kartu],          
                           [appr code],
						   [nomor kartu],
						   [tanggal expired],
						   [shift],
						   [total],
						   [pembulatan],
						   [bayar],
						   [saldo terhutang],                                   
                           [kembali],
						   [kode kasir]) 
    values (@nomor_nota,
	        @tgl_nota,
			@tunai,
			@tgl_jatuh_tempo,
			@paket,
			@grosir,
			@kode_sales,
			@kode_cust,
			@user_id,
			@cara_bayar,
			@jenis_kartu,
			@appr_code,
			@nomor_kartu,
			@tanggal_expired,
			@shift,
			@total,
			@pembulatan,
			@bayar,
			@saldo_terhutang,
			@kembali,
			@kode_kasir)
          
         
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data penjualan'
        return (1)
      End
  
   -- insert data obat_stok
    insert into penjualan_detail ([nomor nota],
	                              [tgl nota],
								  [plu],
								  [kode barang],
								  [paket],
								  [grosir], 
                                  [qty],
								  [kemasan],
								  [satuan],
								  [harga jual],
								  [disc],
								  [discount],
								  [sub total],
								  [kode kasir],
								  [sub plu],
								  [isi])
    select [nomor nota],
	       [tgl nota],
		   [plu],
		   [kode barang],
		   [paket],
		   [grosir], 
           [qty],
		   [kemasan],
		   [satuan],
		   [harga jual],
		   [disc],
		   [discount],
		   [sub total],
		   [kode kasir],
		   [sub plu],
		   [isi] from @PenjualanDetailTVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data penjualan detail'
        return (1)
      End             
    
	-- insert data penjualan qty
    insert into penjualan_qty ([nomor nota],[plu],[kode barang],[qty])
    select [nomor nota],[sub plu],[kode barang],([qty] * [isi]) from @PenjualanDetailTVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data penjualan qty'
        return (1)
      End
	  
	  if @tunai = 'T' 
	  begin
	   insert into piutang ([kode cust],[nomor nota],[tgl nota],[jatuh tempo],[piutang])
	   values (@kode_cust,@nomor_nota,@tgl_nota,@tgl_jatuh_tempo,@saldo_terhutang)

	    set @mERROR_NO = @@ERROR
        if @mERROR_NO <> 0
         begin
          Rollback transaction
          set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data piutang'
          return (1)
         End
	  End               
  commit transaction
 return (0)
 end







GO
/****** Object:  StoredProcedure [dbo].[add_penjualan_tampung]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[add_penjualan_tampung]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @nomor_tampung nchar(25),
   @tunai nchar(1),
   @paket nchar(1),
   @grosir nchar(1),
   @kode_cust nchar(5),
   @kode_sales nchar(5),
   @cust_name nchar(50),
   @user_id nchar(15),
   @nomor_kasir nchar(5),
   @shift nchar(1),
   @PenjualanTampungDetailTVP TVP_Penjualan_Tampung_Detail ReadOnly
    
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert data obat
    insert into penjualan_tampung ([nomor tampung],
	        [tgl nota],
			[tunai],
			[paket],
			[grosir],
			[kode cust],
			[kode sales],
			[cust name], 
            [user id],
			[shift],
			[kode kasir]) 
    values (@nomor_tampung,
	        getdate(),
	        @tunai,
	        @paket,
			@grosir,
			@kode_cust,
			@kode_sales,
			@cust_name,
	        @user_id,
            @shift,
	        @nomor_kasir)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data penjualan tampung'
        return (1)
      End
  
   -- insert data obat_stok
    insert into penjualan_tampung_detail ([nomor tampung],
	                                      [nama barang],
										  [kode barang],
										  [paket],
										  [grosir], 
                                          [qty],
										  [kemasan],
										  [satuan],
										  [harga jual],
										  [disc],
										  [discount],
										  [sub total],
										  [plu])
    select [nomor tampung],
	       [nama barang],
		   [kode barang],
		   [paket],
		   [grosir], 
           [qty],
		   [kemasan],
		   [satuan],
		   [harga jual],
		   [disc],
		   [discount],
		   [sub total],
		   [plu] from @PenjualanTampungDetailTVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data penjualan tampung detail'
        return (1)
      End             
           
  commit transaction
 return (0)
 end









GO
/****** Object:  StoredProcedure [dbo].[add_produsen]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_produsen]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_produsen nchar(5),@nama_produsen nchar(50),
   @alamat nchar(50),@npwp nchar(50),@email_kontak nchar(50),
   @telpon_kontak nchar(50)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into produsen ([kode produsen],[nama produsen],
                         [alamat],[npwp],[email kontak],[telpon kontak])
   Values (@kode_produsen,@nama_produsen,@alamat,@npwp,@email_kontak,@telpon_kontak)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_rekap_kasir]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_rekap_kasir]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),@user_id nchar(15),
   @tgl_transaksi nchar(10),@kode_kasir nchar(5),@shift nchar(10),
   @modal decimal(18,3),@kas_aktual decimal(18,3)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert rekap_suplier
   insert into rekap_kasir ([user id],[tgl transaksi],[kode kasir],[shift],[modal],[kas aktual])
   values (@user_id, @tgl_transaksi,@kode_kasir,@shift,@modal,@kas_aktual)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data rekap kasir'
        return (1)
      End
          
  commit transaction
 return (0)
 end





GO
/****** Object:  StoredProcedure [dbo].[add_sales]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[add_sales]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_sales nchar(5),
   @nama nchar(50),
   @alamat nchar(50),
   @no_hp nchar(15)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into sales ([kode sales],
               [nama],
               [alamat],
			   [no hp])
   Values (@kode_sales,
           @nama,
		   @alamat,
           @no_hp)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end







GO
/****** Object:  StoredProcedure [dbo].[add_stock_report]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[add_stock_report]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @plu nchar(15),@harga_pokok_pembelian decimal(18,2),
   @jumlah_stok decimal(18,2)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into barang_laporan_stok ([plu],[harga pokok pembelian],[jumlah stok])
   Values (@plu,@harga_pokok_pembelian,@jumlah_stok)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian laporan stok'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_sub_kategori]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_sub_kategori]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_kategori nchar(5),
   @kode_sub_kategori nchar(10),
   @nama_sub_kategori nchar(50)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into sub_kategori ([kode kategori],[kode sub kategori],[nama sub kategori])
   Values (@kode_kategori,@kode_sub_kategori, @nama_sub_kategori)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_suplier]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_suplier]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_suplier nchar(5),@nama_suplier nchar(50),
   @alamat nchar(50),@suplier_kena_pajak nchar(1),
   @npwp nchar(50),@email_kontak nchar(50),
   @telpon_kontak nchar(50)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into suplier ([kode suplier],[nama suplier],
                        [alamat],[suplier kena pajak],[npwp],
                        [email kontak],[phone kontak])
   Values (@kode_suplier,@nama_suplier,@alamat,
           @suplier_kena_pajak,@npwp,@email_kontak,@telpon_kontak)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_tabel_bantuan]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_tabel_bantuan]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @jenis_data nchar(50),@keterangan nchar(50)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into tabel_bantuan ([jenis data],[keterangan])
   values (@jenis_data,@keterangan)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  error pada pengisian data tabel bantuan'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[add_users]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[add_users]
   @mERROR_MESSAGE nchar(50) output,
   @rowid int output, 
   @mPROCESS nchar(6), 
   @user_id nchar(15),
   @grup_id nchar(20),
   @password nchar(32),
   @access_control_rule_TVP TVP_access_control_rule ReadOnly
AS

declare @mERROR_NO int
--
if @mPROCESS = 'ADD'
begin

   begin transaction
   insert into users ([user id],[grup id],[password]) 
   values (@user_id,@grup_id,@password)                   
   
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada input data petugas'
        return (1)
      End


   insert into access_control_rule ([user id],[ac_id],[access]) 
   select [user id],[ac_id],[access] from @access_control_rule_TVP

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada input access control rule'
        return (1)
      End

   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'

   DECLARE @rowid_Cursor CURSOR
   SET @rowid_Cursor = CURSOR LOCAL SCROLL FOR 
   SELECT IDENT_CURRENT('users') as rowid 
   OPEN @rowid_Cursor
   FETCH NEXT FROM @rowid_Cursor INTO @rowid
   CLOSE @rowid_Cursor
   DEALLOCATE @rowid_Cursor

   return (0)
End

GO
/****** Object:  StoredProcedure [dbo].[delete_barang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_barang]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @plu nchar(15)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
    -- insert suplier
   delete from barang where [plu] = @plu
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada penghapusan data barang'
        return (1)
      End
      
   delete from barang_stok_awal where [plu] = @plu
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada penghapusan data stok barang'
        return (1)
      End
  
   delete from barang where [sub plu] = @plu
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 3 <  Error pada penghapusan data sub item'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[delete_customer]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_customer]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_cust nchar(10)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
    -- insert suplier
   delete from customer where [kode cust] = @kode_cust
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian delete data customer'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[delete_gudang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_gudang]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_gudang nchar(3)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
    -- delete gudang
   delete from barang_gudang where [kode gudang] = @kode_gudang
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[delete_harga_bertingkat]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_harga_bertingkat]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @plu nchar(15),@kode_barang nchar(25)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
  
    -- insert data barang
   delete from barang_harga_bertingkat where [plu] = @plu and [kode barang] = @kode_barang
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada penghapusan data harga bertingkat'
        return (1)
      End
         
  commit transaction
 return (0)
 end



GO
/****** Object:  StoredProcedure [dbo].[delete_kategori]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_kategori]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_kategori nchar(3)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
    -- insert suplier
   delete from kategori where [kode kategori] = @kode_kategori
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[delete_pembayaran_piutang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[delete_pembayaran_piutang]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_bayar nchar(15)    
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction

    delete from pembayaran_piutang where [kode bayar] = @kode_bayar
	set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada hapus pembayaran data piutang'
        return (1)
      End             
	  commit transaction
 return (0)
 end








GO
/****** Object:  StoredProcedure [dbo].[delete_pembelian]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_pembelian]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @nomor_faktur nchar(15)
   
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction 
    -- delete penjualan
   delete from pembelian where [nomor faktur] = @nomor_faktur             
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data pembelian'
        return (1)
      End
   
   delete from pembelian_detail where [nomor faktur] = @nomor_faktur                
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data pembelian detail'
        return (1)
      End

   delete from pembelian_qty where [nomor faktur] = @nomor_faktur                
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data pembelian qty'
        return (1)
      End

   delete from pembelian_log where [nomor faktur] = @nomor_faktur                
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data pembelian log'
        return (1)
      End
	  	 
   delete from hutang where [nomor faktur] = @nomor_faktur                
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data hutang'
        return (1)
      End
	    	         
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[delete_penjualan]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_penjualan]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @nomor_nota nchar(15)
   
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
  
    -- delete penjualan
   delete from penjualan where [nomor nota] = @nomor_nota                 

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data penjualan'
        return (1)
      End
   
   delete from penjualan_detail where [nomor nota] = @nomor_nota                 

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data penjualan detail'
        return (1)
      End
    
   delete from penjualan_qty where [nomor nota] = @nomor_nota                 

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data penjualan qty'
        return (1)
      End

    delete from penjualan_log where [nomor nota] = @nomor_nota
	set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data penjualan log'
        return (1)
      End
	 
	delete from piutang where [nomor nota] = @nomor_nota
	set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data piutang'
        return (1)
      End      
	     
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[delete_produsen]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_produsen]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_produsen nchar(3)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
    -- insert suplier
   delete from produsen where [kode produsen] = @kode_produsen
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada penghapusan data produsen'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[delete_sales]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[delete_sales]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_sales nchar(5)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
    -- insert suplier
   delete from sales where [kode sales] = @kode_sales
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian delete data customer'
        return (1)
      End
           
  commit transaction
 return (0)
 end







GO
/****** Object:  StoredProcedure [dbo].[delete_sub_kategori]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_sub_kategori]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_sub_kategori nchar(6)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
    -- insert suplier
   delete from sub_kategori where [kode sub kategori]  = @kode_sub_kategori
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada penghapusan sub kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[delete_suplier]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_suplier]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_suplier nchar(5)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
    -- insert suplier
   delete from suplier where [kode suplier] = @kode_suplier
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian delete data suplier'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[delete_tabel_bantuan]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_tabel_bantuan]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @rowid int
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'DELETE'
begin
  begin transaction
    -- insert suplier
   delete from tabel_bantuan where [id] = @rowid
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  error pada pengisian data tabel bantuan'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[delete_users]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_users]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @user_id nchar(15)
AS

declare @mERROR_NO int
--
if @mPROCESS = 'DELETE'
begin
   begin transaction
   delete from users 
   where [user id] = @user_id 
     
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada delete data petugas'
        return (1)
      End

   delete from access_control_rule where [user id] = @user_id
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada hapus data access control'
        return (1)
      End
	    
   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End






GO
/****** Object:  StoredProcedure [dbo].[laporan_stok_minimum]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[laporan_stok_minimum]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_obat nchar(25),
   @harga_pokok_pembelian decimal(18,2),
   @stok_minimum decimal(18,2),
   @jumlah_stok decimal(18,2)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'ADD'
begin
  begin transaction
    -- insert suplier
   insert into obat_laporan_stok_minimum ([kode obat],[harga pokok pembelian],
                                          [stok minimum],[jumlah stok])
   Values (@kode_obat,@harga_pokok_pembelian,@stok_minimum , @jumlah_stok)
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian laporan stok'
        return (1)
      End
           
  commit transaction
 return (0)
 end





GO
/****** Object:  StoredProcedure [dbo].[update_barang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[update_barang]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @old_plu nchar(15),
   @kode_barang nchar(25),
   @plu nchar(15), 
   @nama_barang nchar(50),
   @kode_kategori nchar(5),@kode_sub_kategori nchar(10),
   @kode_produsen nchar(5),@kode_suplier nchar(5),
   @bentuk_sediaan nchar(50),@satuan_terbesar nchar(50),
   @isi decimal(18,2),@satuan_terkecil nchar(50),
   @harga_pokok_pembelian decimal(18,2),
   @ppn_masuk decimal(18,2),
   @harga_pokok_penjualan decimal(18,2),
   @harga_jual_toko decimal(18,2),
   @harga_jual_grosir decimal(18,2),
   @kena_pajak nchar(1),
   @stok_minimum decimal(18,2),
   @Barang_Stok_TVP TVP_Barang_Stoks ReadOnly,
   @Barang_Sub_Item_TVP TVP_Barang_Sub_Item ReadOnly    
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction

    -- insert data obat
    update barang set [kode barang] = @kode_barang,
	            [plu] = @plu,  
	            [nama barang] = @nama_barang,
				[kode kategori] = @kode_kategori,
                [kode sub kategori] = @kode_sub_kategori, 
                [kode produsen] = @kode_produsen,
                [kode suplier] = @kode_suplier, 
                [bentuk sediaan] = @bentuk_sediaan,
                [satuan terbesar] = @satuan_terbesar,
                [isi] = @isi,
                [satuan terkecil] = @satuan_terkecil,
                [harga pokok pembelian] = @harga_pokok_pembelian,
                [ppn masuk] = @ppn_masuk,
                [harga pokok penjualan] = @harga_pokok_penjualan, 
                [harga jual toko] = @harga_jual_toko,
                [harga jual grosir] = @harga_jual_grosir,
                [kena pajak] = @kena_pajak,
                [stok minimum] = @stok_minimum
    where [plu] = @old_plu                  
      
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data barang'
        return (1)
      End
      
   -- delete from kode obat
   delete from barang_stok_awal where [plu] = @old_plu

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data barang'
        return (1)
      End

   -- insert data obat_stok
    insert into barang_stok_awal ([kode barang],[plu],[kode gudang],[stok awal],[tgl stok],[user id])
    select [kode barang],[plu],[kode gudang],[stok awal],[tgl stok],[user id] from @Barang_Stok_TVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data ijin'
        return (1)
      End
	  
	  -- delete from barang for sub item
	  delete from barang where [sub plu] = @old_plu

      set @mERROR_NO = @@ERROR
      if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada delete data barang'
        return (1)
      End

	  -- insert into barang for sub item
	  insert into barang ([kode barang],
	                  [plu],
	                  [nama barang],
					  [satuan terbesar],
                      [isi],
					  [satuan terkecil],
					  [harga pokok pembelian],
					  [ppn masuk],
                      [harga pokok penjualan],
					  [harga jual toko],
					  [harga jual grosir],
					  [sub item],
					  [sub plu])
    select [kode barang],
	                  [plu],
	                  [nama barang],
					  [satuan terbesar],
                      [isi],
					  [satuan terkecil],
					  [harga pokok pembelian],
					  [ppn masuk],
                      [harga pokok penjualan],
					  [harga jual toko],
					  [harga jual grosir],
					  [sub item],
					  [sub plu]
    from @barang_sub_item_TVP

	set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data ijin'
        return (1)
      End                 
           
  commit transaction
 return (0)
 end







GO
/****** Object:  StoredProcedure [dbo].[update_customer]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_customer]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_cust nchar(10),
   @nama nchar(50),
   @alamat nchar(50),
   @no_hp nchar(15)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
    -- insert suplier
   update customer set [nama] = @nama,
               [alamat] = @alamat,
			   [no hp] =  @no_hp
   where [kode cust] = @kode_cust
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian update data customer'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[update_gudang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_gudang]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_gudang nchar(3),@nama_gudang nchar(50)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
    -- insert suplier
   update barang_gudang set [nama gudang] = @nama_gudang where [kode gudang] = @kode_gudang           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada update data gudang'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[update_harga_barang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_harga_barang]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @plu nchar(7),
   @harga_pokok_pembelian decimal(18,2),
   @kena_pajak nchar(1),
   @ppn_masuk decimal(18,2),
   @harga_pokok_penjualan decimal(18,2),
   @harga_jual_toko decimal(18,2),
   @harga_jual_grosir decimal(18,2)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
  
    -- insert suplier
   update barang set [harga pokok pembelian] = @harga_pokok_pembelian,
   [ppn masuk] = @ppn_masuk,
   [kena pajak] = @kena_pajak, 
   [harga pokok penjualan] = @harga_pokok_penjualan,
   [harga jual toko] = @harga_jual_toko,
   [harga jual grosir] = @harga_jual_grosir
   where [plu] = @plu  
          
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada update harga obat'
        return (1)
      End         
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[update_harga_bertingkat]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_harga_bertingkat]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @plu nchar(7),@kode_barang nchar(25),
   @HargaBarangBertingkatTVP TVP_Barang_Harga_Bertingkats ReadOnly
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
  
   -- insert data barang
   delete from barang_harga_bertingkat where [plu] = @plu and [kode barang] = @kode_barang
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada penghapusan data harga bertingkat'
        return (1)
      End
      
    -- insert data barang
   insert into barang_harga_bertingkat([plu],[kode barang],[qty min],[qty max],[harga jual])
   select [plu],[kode barang],[qty min],[qty max],[harga jual] from @HargaBarangBertingkatTVP
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data tampung penjualan'
        return (1)
      End
         
  commit transaction
 return (0)
 end



GO
/****** Object:  StoredProcedure [dbo].[update_harga_obat]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_harga_obat]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_obat nchar(25),@harga_pokok_pembelian decimal(18,2),
   @kena_pajak nchar(1),@ppn_masuk decimal(18,2),
   @harga_pokok_penjualan decimal(18,2),
   @harga_jual_bebas decimal(18,2),
   @harga_jual_resep decimal(18,2)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
  
    -- insert suplier
   update obat set [harga pokok pembelian] = @harga_pokok_pembelian ,
   [ppn masuk] = @ppn_masuk,[kena pajak] = @kena_pajak, [harga pokok penjualan] = @harga_pokok_penjualan,
   [harga jual bebas] = @harga_jual_bebas,[harga jual resep] = @harga_jual_resep
   where [kode obat] = @kode_obat  
          
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada update harga obat'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[update_kategori]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_kategori]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_kategori nchar(5),@nama_kategori nchar(50),@rowid int
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
    -- insert suplier
   update kategori set [kode kategori] = @kode_kategori, [nama kategori] = @nama_kategori where [id] = @rowid
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada update data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[update_pembayaran_piutang]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[update_pembayaran_piutang]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_bayar nchar(15),
   @pembayaran_piutang_TVP TVP_pembayaran_piutang ReadOnly     
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction

    delete from pembayaran_piutang where [kode bayar] = @kode_bayar
	set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada hapus pembayaran data piutang'
        return (1)
      End             
   
    insert into pembayaran_piutang([kode bayar],
	            [kode cust],
				[nomor nota],
				[tgl],
				[bayar])
    select [kode bayar],
	       [kode cust],
		   [nomor nota],
		   [tgl],
		   [bayar] from @pembayaran_piutang_TVP         
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pembayaran data piutang'
        return (1)
      End             
    
	  commit transaction
 return (0)
 end








GO
/****** Object:  StoredProcedure [dbo].[update_pembelian]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_pembelian]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @nomor_faktur nchar(15),  
   @old_nomor_faktur nchar(15),
   @kode_suplier nchar(5),
   @tgl_faktur nchar(10),@jenis nchar(1),
   @tgl_terima nchar(10),@tgl_jatuh_tempo nchar(10),
   @biaya_lain decimal(18,2),@total_pajak decimal(18,2),
   @uang_muka decimal(18,2),@saldo_terhutang decimal(18,2),
   @user_id nchar(15),
   @PembelianDetailTVP TVP_Pembelian_Details ReadOnly    
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
    -- insert data obat
    update pembelian set [nomor faktur] = @nomor_faktur,
                     	 [kode suplier] = @kode_suplier, 
                     	 [tgl faktur] = @tgl_faktur,
                     	 [jenis] = @jenis, 
                     	 [tgl terima] = @tgl_terima, 
                     	 [tgl jatuh tempo] = @tgl_jatuh_tempo,
                     	 [biaya lain] = @biaya_lain,
                     	 [total pajak] = @total_pajak,
                     	 [uang muka] = @uang_muka,
                     	 [saldo terhutang] = @saldo_terhutang,
                     	 [user id] = @user_id
    where [nomor faktur] = @old_nomor_faktur
         
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data pembelian obat'
        return (1)
      End
  
   -- delete from pembelian_detail
   delete from pembelian_detail where [nomor faktur] = @old_nomor_faktur
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada penghapusan data pembelian barang'
        return (1)
      End

   -- delete from pembelian_detail
   delete from pembelian_qty where [nomor faktur] = @old_nomor_faktur
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada penghapusan data pembelian barang'
        return (1)
      End

   -- insert data obat_stok
    insert into pembelian_detail ([nomor faktur],[plu],[kode barang],[qty],[satuan],
                                [harga pokok pembelian],[disc],[discount],[sub total],
                                [pajak],[besar pajak])
    select [nomor faktur],[plu],[kode barang],[qty],[satuan],
	       [harga pokok pembelian],[disc],[discount],
           [sub total],[pajak],[besar pajak] from @PembelianDetailTVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 3 <  Error pada pengisian data pembelian detail'
        return (1)
      End       
	        
  -- insert data pembelian qty
    insert into pembelian_qty ([nomor faktur],[plu],[kode barang],[qty])
    select [nomor faktur],[plu],[kode barang],[qty] from @PembelianDetailTVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 3 <  Error pada pengisian data pembelian qty'
        return (1)
      End           
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[update_penjualan]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_penjualan]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @nomor_nota_awal nchar(15),
   @nomor_nota nchar(15),
   @tgl_nota nchar(20),
   @kode_sales nchar(10),
   @kode_cust nchar(10),
   @tunai nchar(1),
   @tgl_jatuh_tempo nchar(10),
   @paket nchar(1),
   @grosir nchar(1),
   @user_id nchar(15),
   @kode_kasir nchar(5),
   @shift nchar(10),
   @PenjualanDetailTVP TVP_Penjualan_Detail_ ReadOnly,
   @total decimal(18,2),
   @pembulatan decimal(18,2),
   @bayar decimal(18,2),
   @saldo_terhutang decimal(18,2),
   @kembali decimal(18,2)    
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
    -- insert data obat
    update penjualan set [nomor nota] = @nomor_nota,
                         [tgl nota] = @tgl_nota,
						 [kode sales] = @kode_sales,
						 [kode cust] = @kode_cust,
                         [tunai] = @tunai,
						 [jatuh tempo] = @tgl_jatuh_tempo,
                         [paket] = @paket,
						 [grosir] = @grosir, 
                         [user id] = @user_id,
                         [shift] = @shift,
                         [total] = @total,
						 [pembulatan] = @pembulatan,
						 [bayar] = @bayar,
						 [saldo terhutang] = @saldo_terhutang,
						 [kembali] = @kembali,
                         [kode kasir] = @kode_kasir 
    where [nomor nota] = @nomor_nota_awal
            
         
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data penjualan'
        return (1)
      End
  

   --- delete from penjualan_detail
   delete from penjualan_detail where [nomor nota] = @nomor_nota_awal
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data penjualan'
        return (1)
      End
   
    --- delete from penjualan_detail
   delete from penjualan_qty where [nomor nota] = @nomor_nota_awal
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data penjualan'
        return (1)
      End

    --- delete from piutang
   delete from piutang where [nomor nota] = @nomor_nota_awal
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data penjualan'
        return (1)
      End

    --- delete from pembayaran piutang
   delete from pembayaran_piutang where [nomor nota] = @nomor_nota_awal
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data penjualan'
        return (1)
    End

	if @tunai = 'T' 
	  begin
	   insert into piutang ([kode cust],[nomor nota],[tgl nota],[jatuh tempo],[piutang])
	   values (@kode_cust,@nomor_nota,@tgl_nota,@tgl_jatuh_tempo,@saldo_terhutang)

	    set @mERROR_NO = @@ERROR
        if @mERROR_NO <> 0
         begin
          Rollback transaction
          set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data piutang'
          return (1)
         End
	  End            

   -- insert penjualan detail
    insert into penjualan_detail ([nomor nota],
	           [tgl nota],
			   [plu],
			   [kode barang],
			   [paket],
			   [grosir], 
               [qty],
			   [kemasan],
			   [satuan],
			   [harga jual],
			   [disc],
			   [discount],
			   [sub total],
			   [kode kasir],
			   [sub plu],
			   [isi])
    select [nomor nota],
	       [tgl nota],
		   [plu],
		   [kode barang],
		   [paket],
		   [grosir], 
           [qty],
		   [kemasan],
		   [satuan],
		   [harga jual],
		   [disc],
		   [discount],
		   [sub total],
		   [kode kasir],
		   [sub plu],
		   [isi] from @PenjualanDetailTVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data penjualan detail'
        return (1)
      End             
       
	-- insert penjualan qty
    insert into penjualan_qty ([nomor nota],[plu],[kode barang],[qty])
    select [nomor nota],[plu],[kode barang],([qty] * [isi]) from @PenjualanDetailTVP
           
    
    set @mERROR_NO = @@ERROR
    if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 2 <  Error pada pengisian data penjualan detail'
        return (1)
      End        
	      
  commit transaction
 return (0)
 end







GO
/****** Object:  StoredProcedure [dbo].[update_produsen]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_produsen]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_produsen nchar(5),@nama_produsen nchar(50),
   @alamat nchar(50),@npwp nchar(50),@email_kontak nchar(50),
   @telpon_kontak nchar(50),@rowid int
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
    -- insert suplier
   update produsen set [kode produsen] = @kode_produsen,
                       [nama produsen] = @nama_produsen,
                       [alamat] = @alamat,
                       [npwp] = @npwp,
                       [email kontak] = @email_kontak,
                       [telpon kontak] = @telpon_kontak
   where [id] = @rowid
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada update data produsen'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[update_sales]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[update_sales]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6),
   @kode_sales nchar(5),
   @nama nchar(50),
   @alamat nchar(50),
   @no_hp nchar(15)
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
    -- insert suplier
   update sales set [nama] = @nama,
               [alamat] = @alamat,
			   [no hp] =  @no_hp
   where [kode sales] = @kode_sales
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian update data sales'
        return (1)
      End
           
  commit transaction
 return (0)
 end







GO
/****** Object:  StoredProcedure [dbo].[update_sub_kategori]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_sub_kategori]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_kategori nchar(5),
   @kode_sub_kategori nchar(10),
   @nama_sub_kategori nchar(50),
   @rowid int
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
    -- insert suplier
   update sub_kategori set [kode kategori] = @kode_kategori,
                           [kode sub kategori] = @kode_sub_kategori,
                           [nama sub kategori] = @nama_sub_kategori
   where [id] = @rowid                        
                                   
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[update_suplier]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_suplier]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @kode_suplier nchar(5),@nama_suplier nchar(50),
   @alamat nchar(50),@suplier_kena_pajak nchar(1),
   @npwp nchar(50),@email_kontak nchar(50),
   @telpon_kontak nchar(50),@rowid int
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
    -- insert suplier
   update suplier set [kode suplier] = @kode_suplier,
                      [nama suplier] = @nama_suplier,
                      [alamat] = @alamat,
                      [suplier kena pajak] = @suplier_kena_pajak,
                      [npwp] = @npwp,
                      [email kontak] = @email_kontak,
                      [phone kontak] = @telpon_kontak
   where [id] = @rowid
                              
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  Error pada pengisian data kategori'
        return (1)
      End
           
  commit transaction
 return (0)
 end






GO
/****** Object:  StoredProcedure [dbo].[update_tabel_bantuan]    Script Date: 02/04/2021 15:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_tabel_bantuan]
   @mERROR_MESSAGE nchar(50) output, @mPROCESS nchar(6),
   @jenis_data nchar(50),@keterangan nchar(50),@rowid int
AS
 Declare @mERROR_NO int
 --
if @mPROCESS = 'UPDATE'
begin
  begin transaction
    -- insert suplier
   update tabel_bantuan set [jenis data] = @jenis_data,
          [keterangan] = @keterangan where [id] = @rowid
           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' > SP 1 <  error pada pengisian data tabel bantuan'
        return (1)
      End
           
  commit transaction
 return (0)
 end

GO
