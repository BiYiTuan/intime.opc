using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Intime.OPC.CustomControls
{
    /// <summary>
    ///     分页DataGrid
    /// </summary>
    [TemplatePart(Name = ElementFirstPageImageButton, Type = typeof (ImageButton))]
    [TemplatePart(Name = ElementPerviousPageImageButton, Type = typeof (ImageButton))]
    [TemplatePart(Name = ElementNextPageImageButton, Type = typeof (ImageButton))]
    [TemplatePart(Name = ElementLastPageImageButton, Type = typeof (ImageButton))]
    [TemplatePart(Name = ElementRefreshImageButton, Type = typeof (ImageButton))]
    [TemplatePart(Name = ElementPageSizeListComBox, Type = typeof (ComboBox))]
    [TemplatePart(Name = ElementPageIndexTextBox, Type = typeof (TextBox))]
    [TemplatePart(Name = ElementPageCountTextBlock, Type = typeof (TextBlock))]
    [TemplatePart(Name = ElementStartTextBlock, Type = typeof (TextBlock))]
    [TemplatePart(Name = ElementEndTextBlock, Type = typeof (TextBlock))]
    [TemplatePart(Name = ElementCountTextBlock, Type = typeof (TextBlock))]
    public class PagingDataGrid : DataGrid
    {
        static PagingDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (PagingDataGrid),
                new FrameworkPropertyMetadata(typeof (PagingDataGrid)));
        }

        public PagingDataGrid()
        {
            CanUserAddRows = false;
        }

        #region 字段和属性

        public delegate void PagingChangedEventHandler(object sender, PagingChangedEventArgs args);

        private const string ElementFirstPageImageButton = "PART_FirstPage";
        private const string ElementPerviousPageImageButton = "PART_PerviousPage";
        private const string ElementNextPageImageButton = "PART_NextPage";
        private const string ElementLastPageImageButton = "PART_LastPage";
        private const string ElementRefreshImageButton = "PART_Refresh";

        private const string ElementPageSizeListComBox = "PART_PageSizeList";
        private const string ElementPageIndexTextBox = "PART_PageIndex";

        private const string ElementPageCountTextBlock = "PART_PageCount";
        private const string ElementStartTextBlock = "PART_Start";
        private const string ElementEndTextBlock = "PART_End";
        private const string ElementCountTextBlock = "PART_Count";


        private ImageButton btnFirst;
        private ImageButton btnLast;
        private ImageButton btnNext;
        private ImageButton btnPrevious;
        private ImageButton btnRefresh;
        private ComboBox cboPageSize;
        private PagingChangedEventArgs pagingChangedEventArgs;
        private TextBlock tbCount;
        private TextBlock tbEnd;
        private TextBlock tbPageCount, tbStart;
        private TextBox txtPageIndex;

        #endregion

        #region 依赖属性

        // Using a DependencyProperty as the backing store for IsShowRowHeaderNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowRowHeaderNumberProperty =
            DependencyProperty.Register("IsShowRowHeaderNumber", typeof (bool), typeof (PagingDataGrid),
                new UIPropertyMetadata(true));


        /// <summary>
        ///     是否显示分页
        /// </summary>
        public static readonly DependencyProperty IsShowPagingProperty =
            DependencyProperty.Register("IsShowPaging", typeof (bool), typeof (PagingDataGrid),
                new UIPropertyMetadata(true));


        /// <summary>
        ///     是否允许分页
        /// </summary>
        public static readonly DependencyProperty AllowPagingProperty =
            DependencyProperty.Register("AllowPaging", typeof (bool), typeof (PagingDataGrid),
                new UIPropertyMetadata(true));


        /// <summary>
        ///     显示每页记录数字符串列表
        ///     例:10,20,30
        /// </summary>
        public static readonly DependencyProperty PageSizeListProperty =
            DependencyProperty.Register("PageSizeList", typeof (string), typeof (PagingDataGrid),
                new UIPropertyMetadata(null, (s, e) =>
                {
                    var dp = s as PagingDataGrid;
                    if (dp.PageSizeItemsSource == null)
                    {
                        dp.PageSizeItemsSource = new List<int>();
                    }
                    if (dp.PageSizeItemsSource != null)
                    {
                        List<string> strs =
                            e.NewValue.ToString().Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                        dp.PageSizeItemsSource.Clear();
                        strs.ForEach(c => { dp.PageSizeItemsSource.Add(Convert.ToInt32(c)); });
                    }
                }));


        /// <summary>
        ///     显示每页记录数集合
        /// </summary>
        protected static readonly DependencyProperty PageSizeItemsSourceProperty =
            DependencyProperty.Register("PageSizeItemsSource", typeof (IList<int>), typeof (PagingDataGrid),
                new UIPropertyMetadata(new List<int> {5, 10, 20, 30, 50}));


        /// <summary>
        ///     总记录数
        /// </summary>
        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof (int), typeof (PagingDataGrid), new UIPropertyMetadata(0));


        /// <summary>
        ///     每页记录数，默认：10
        /// </summary>
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof (int), typeof (PagingDataGrid), new UIPropertyMetadata(10));


        /// <summary>
        ///     当前页码，默认：1
        /// </summary>
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof (int), typeof (PagingDataGrid), new UIPropertyMetadata(1));


        /// <summary>
        ///     总页数
        /// </summary>
        protected static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof (int), typeof (PagingDataGrid), new UIPropertyMetadata(10));


        /// <summary>
        ///     起始记录数
        /// </summary>
        protected static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof (int), typeof (PagingDataGrid), new UIPropertyMetadata(0));


        /// <summary>
        ///     终止记录数
        /// </summary>
        protected static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof (int), typeof (PagingDataGrid), new UIPropertyMetadata(0));


        /// <summary>
        ///     排序字段
        /// </summary>
        public static readonly DependencyProperty SortFieldProperty =
            DependencyProperty.Register("SortField", typeof (string), typeof (PagingDataGrid),
                new UIPropertyMetadata(null));

        public static readonly RoutedEvent PagingChangedEvent = EventManager.RegisterRoutedEvent("PagingChangedEvent",
            RoutingStrategy.Bubble, typeof (PagingChangedEventHandler), typeof (PagingDataGrid));

        public bool IsShowRowHeaderNumber
        {
            get { return (bool) GetValue(IsShowRowHeaderNumberProperty); }
            set { SetValue(IsShowRowHeaderNumberProperty, value); }
        }

        public bool IsShowPaging
        {
            get { return (bool) GetValue(IsShowPagingProperty); }
            set { SetValue(IsShowPagingProperty, value); }
        }

        public bool AllowPaging
        {
            get { return (bool) GetValue(AllowPagingProperty); }
            set { SetValue(AllowPagingProperty, value); }
        }

        public string PageSizeList
        {
            get { return (string) GetValue(PageSizeListProperty); }
            set { SetValue(PageSizeListProperty, value); }
        }

        protected IList<int> PageSizeItemsSource
        {
            get { return (IList<int>) GetValue(PageSizeItemsSourceProperty); }
            set { SetValue(PageSizeItemsSourceProperty, value); }
        }

        public int Total
        {
            get { return (int) GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        public int PageSize
        {
            get { return (int) GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public int PageIndex
        {
            get { return (int) GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        protected int PageCount
        {
            get { return (int) GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        protected int Start
        {
            get { return (int) GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        protected int End
        {
            get { return (int) GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        public string SortField
        {
            get { return (string) GetValue(SortFieldProperty); }
            set { SetValue(SortFieldProperty, value); }
        }

        /// <summary>
        ///     分页事件
        /// </summary>
        public event PagingChangedEventHandler PagingChanged
        {
            add { AddHandler(PagingChangedEvent, value); }
            remove { RemoveHandler(PagingChangedEvent, value); }
        }

        #endregion

        #region 重写方法

        /// <summary>
        ///     应用样式
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            btnFirst = GetTemplateChild(ElementFirstPageImageButton) as ImageButton;
            btnPrevious = GetTemplateChild(ElementPerviousPageImageButton) as ImageButton;
            btnNext = GetTemplateChild(ElementNextPageImageButton) as ImageButton;
            btnLast = GetTemplateChild(ElementLastPageImageButton) as ImageButton;
            btnRefresh = GetTemplateChild(ElementRefreshImageButton) as ImageButton;

            cboPageSize = GetTemplateChild(ElementPageSizeListComBox) as ComboBox;
            txtPageIndex = GetTemplateChild(ElementPageIndexTextBox) as TextBox;

            tbPageCount = GetTemplateChild(ElementPageCountTextBlock) as TextBlock;
            tbStart = GetTemplateChild(ElementStartTextBlock) as TextBlock;
            tbEnd = GetTemplateChild(ElementEndTextBlock) as TextBlock;
            tbCount = GetTemplateChild(ElementCountTextBlock) as TextBlock;

            btnFirst.Click += btnFirst_Click;
            btnLast.Click += btnLast_Click;
            btnNext.Click += btnNext_Click;
            btnPrevious.Click += btnPrevious_Click;
            btnRefresh.Click += btnRefresh_Click;

            cboPageSize.SelectionChanged += cboPageSize_SelectionChanged;
            txtPageIndex.PreviewKeyDown += txtPageIndex_PreviewKeyDown;
            txtPageIndex.LostFocus += txtPageIndex_LostFocus;

            Loaded += PagingDataGrid_Loaded;
        }

        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            base.OnLoadingRow(e);

            if (IsShowRowHeaderNumber)
            {
                e.Row.Header = e.Row.GetIndex() + 1;
            }
        }

        #endregion

        #region 分页事件

        private void RaisePageChanged()
        {
            if (pagingChangedEventArgs == null)
            {
                pagingChangedEventArgs = new PagingChangedEventArgs(PagingChangedEvent, PageSize, PageIndex);
            }

            if (AllowPaging)
            {
                pagingChangedEventArgs.PageSize = PageSize;
                pagingChangedEventArgs.PageIndex = PageIndex;
            }
            else
            {
                pagingChangedEventArgs.PageSize = PageSize = int.MaxValue;
                pagingChangedEventArgs.PageIndex = 1;
            }

            RaiseEvent(pagingChangedEventArgs);

            //calc start、end
            if (ItemsSource != null)
            {
                int curCount = 0;
                IEnumerator enumrator = ItemsSource.GetEnumerator();
                while (enumrator.MoveNext())
                {
                    curCount++;
                }

                //不允许分页处理
                if (!AllowPaging)
                {
                    PageSizeItemsSource.Clear();
                    PageSizeItemsSource.Add(curCount);
                    PageSize = curCount;
                }

                Start = (PageIndex - 1)*PageSize + 1;
                End = Start + curCount - 1;

                if (Total%PageSize != 0)
                {
                    PageCount = Total/PageSize + 1;
                }
                else
                {
                    PageCount = Total/PageSize;
                }
            }
            else
            {
                Start = End = PageCount = Total = 0;
            }

            //调整图片的显示
            btnFirst.IsEnabled = btnPrevious.IsEnabled = (PageIndex != 1);
            btnNext.IsEnabled = btnLast.IsEnabled = (PageIndex != PageCount);
        }

        private void PagingDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            RaisePageChanged();
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = 1;
            RaisePageChanged();
        }


        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (PageIndex > 1)
            {
                --PageIndex;
            }
            RaisePageChanged();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (Total%PageSize != 0)
            {
                PageCount = Total/PageSize + 1;
            }
            else
            {
                PageCount = Total/PageSize;
            }

            if (PageIndex < PageCount)
            {
                ++PageIndex;
            }
            RaisePageChanged();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            if (Total%PageSize != 0)
            {
                PageCount = Total/PageSize + 1;
            }
            else
            {
                PageCount = Total/PageSize;
            }
            PageIndex = PageCount;
            RaisePageChanged();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RaisePageChanged();
        }

        private void txtPageIndex_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPageIndex_LostFocus(sender, null);
            }
        }

        private void txtPageIndex_LostFocus(object sender, RoutedEventArgs e)
        {
            int pIndex = 0;
            try
            {
                pIndex = Convert.ToInt32(txtPageIndex.Text);
            }
            catch
            {
                pIndex = 1;
            }

            if (pIndex < 1) PageIndex = 1;
            else if (pIndex > PageCount) PageIndex = PageCount;
            else PageIndex = pIndex;

            RaisePageChanged();
        }

        private void cboPageSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                PageSize = (int) cboPageSize.SelectedItem;
                RaisePageChanged();
            }
        }

        #endregion
    }

    public class PagingChangedEventArgs : RoutedEventArgs
    {
        public PagingChangedEventArgs(RoutedEvent eventToRaise, int pageSize, int pageIndex, string sort = null)
            : base(eventToRaise)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Sort = sort;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Sort { get; set; }
    }
}