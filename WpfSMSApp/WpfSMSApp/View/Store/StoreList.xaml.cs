using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfSMSApp.Model;

namespace WpfSMSApp.View.Store
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StoreList : Page
    {
        public StoreList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Store 테이블 데이터를 읽어와야 함
                List<Model.Store> stores = new List<Model.Store>();
                List<Model.StockStore> stockStores = new List<Model.StockStore>();
                List<Model.Stock> stocks = new List<Model.Stock>();

                stores = Logic.DataAccess.GetStores();
                stocks = Logic.DataAccess.GetStocks();

                //stores 데이터를 stockstores로 옮겨야 함(stores가 원본 데이터)
                foreach (Model.Store item in stores)
                {
                    var store = new Model.StockStore()
                    {
                        StoreID = item.StoreID,
                        StoreName = item.StoreName,
                        StoreLocation = item.StoreLocation,
                        ItemStatus = item.ItemStatus,
                        TagID = item.TagID,
                        BarcodeID = item.BarcodeID,
                        StockQuantitiy = 0
                    };

                    store.StockQuantitiy = Logic.DataAccess.GetStocks().Where(t => t.Store.Equals(store.StoreID)).Count();
                    stockStores.Add(store);   
                }

                this.DataContext = stores;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 StoreList Loaded : {ex}");
                throw ex;
            }
        }
        
        private void BtnAddStore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new AddStore());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnAddStore_Click : {ex}");
                throw ex;
            }
        }

        private void BtnEditStore_Click(object sender, RoutedEventArgs e)
        {
            if(GrdData.SelectedItem == null)
            {
                Commons.ShowMessageAsync("창고수정", "수정할 창고를 선택하세요.");
                return;
            }
            try
            {
                var storeID = (GrdData.SelectedItem as Model.Store).StoreID;
                NavigationService.Navigate(new EditStore(storeID));
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnEditStore_Click : {ex}");
                throw ex;
            }
        }
        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog Dialog = new SaveFileDialog();
            Dialog.Filter = "Excel File (*.xlsx)|*.xlsx"; //엑셀 확장자
            Dialog.FileName = "";
            if (Dialog.ShowDialog() == true)
            {
                try
                {
                    IWorkbook workbook = new XSSFWorkbook(); //xlsx용, HSSFWorkbook : xls(이전버전용)
                    ISheet sheet = workbook.CreateSheet("Sheet1"); //이름변경 가능
                                                                   //header row 생성
                    IRow rowHeader = sheet.CreateRow(0);
                    ICell cell = rowHeader.CreateCell(0);
                    cell.SetCellValue("순번");
                    cell = rowHeader.CreateCell(1);
                    cell.SetCellValue("창고명");
                    cell = rowHeader.CreateCell(2);
                    cell.SetCellValue("창고위치");
                    cell = rowHeader.CreateCell(3);
                    cell.SetCellValue("재고수");

                    for (int i = 0; i < GrdData.Items.Count; i++)
                    {
                        IRow row = sheet.CreateRow(i + 1);
                        if(GrdData.Items[i] is Model.StockStore)
                        {
                            var stockstore = GrdData.Items[i] as Model.StockStore;
                            ICell dataCell = row.CreateCell(0);
                            dataCell.SetCellValue(stockstore.StoreID);
                            dataCell = row.CreateCell(1);
                            dataCell.SetCellValue(stockstore.StoreName);
                            dataCell = row.CreateCell(2);
                            dataCell.SetCellValue(stockstore.StoreLocation);
                            dataCell = row.CreateCell(3);
                            dataCell.SetCellValue(stockstore.StockQuantitiy);
                        }
                    }

                    //파일저장
                    using (var fs = new FileStream(Dialog.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        workbook.Write(fs);
                    }

                    Commons.ShowMessageAsync("엑셀 저장", "엑셀 export 성공!");
                }
                catch (Exception ex)
                {
                    Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
                }
            }
        }

    }
}