using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Intime.OPC.Data.GenerateModel.Models.Mapping;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class YintaiHZhouContext : DbContext
    {
        static YintaiHZhouContext()
        {
            Database.SetInitializer<YintaiHZhouContext>(null);
        }

        public YintaiHZhouContext()
            : base("Name=YintaiHZhouContext")
        {
        }

        public DbSet<AdminAccessRight> AdminAccessRights { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardBlack> CardBlacks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryMap> CategoryMaps { get; set; }
        public DbSet<CategoryProperty> CategoryProperties { get; set; }
        public DbSet<CategoryPropertyValue> CategoryPropertyValues { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ConfigMsg> ConfigMsgs { get; set; }
        public DbSet<CouponHistory> CouponHistories { get; set; }
        public DbSet<CouponLog> CouponLogs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DeviceLog> DeviceLogs { get; set; }
        public DbSet<DeviceToken> DeviceTokens { get; set; }
        public DbSet<ExOrder> ExOrders { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<HotWord> HotWords { get; set; }
        public DbSet<IMS_Associate> IMS_Associate { get; set; }
        public DbSet<IMS_AssociateBrand> IMS_AssociateBrand { get; set; }
        public DbSet<IMS_AssociateIncome> IMS_AssociateIncome { get; set; }
        public DbSet<IMS_AssociateIncomeHistory> IMS_AssociateIncomeHistory { get; set; }
        public DbSet<IMS_AssociateIncomeRequest> IMS_AssociateIncomeRequest { get; set; }
        public DbSet<IMS_AssociateIncomeRule> IMS_AssociateIncomeRule { get; set; }
        public DbSet<IMS_AssociateIncomeRuleFix> IMS_AssociateIncomeRuleFix { get; set; }
        public DbSet<IMS_AssociateIncomeRuleFlatten> IMS_AssociateIncomeRuleFlatten { get; set; }
        public DbSet<IMS_AssociateIncomeRuleFlex> IMS_AssociateIncomeRuleFlex { get; set; }
        public DbSet<IMS_AssociateIncomeTran2Req> IMS_AssociateIncomeTran2Req { get; set; }
        public DbSet<IMS_AssociateIncomeTransfer> IMS_AssociateIncomeTransfer { get; set; }
        public DbSet<IMS_AssociateItems> IMS_AssociateItems { get; set; }
        public DbSet<IMS_AssociateSaleCode> IMS_AssociateSaleCode { get; set; }
        public DbSet<IMS_Bank> IMS_Bank { get; set; }
        public DbSet<IMS_Combo> IMS_Combo { get; set; }
        public DbSet<IMS_Combo2Product> IMS_Combo2Product { get; set; }
        public DbSet<IMS_GiftCard> IMS_GiftCard { get; set; }
        public DbSet<IMS_GiftCardItem> IMS_GiftCardItem { get; set; }
        public DbSet<IMS_GiftCardOrder> IMS_GiftCardOrder { get; set; }
        public DbSet<IMS_GiftCardRecharge> IMS_GiftCardRecharge { get; set; }
        public DbSet<IMS_GiftCardTransfers> IMS_GiftCardTransfers { get; set; }
        public DbSet<IMS_GiftCardUser> IMS_GiftCardUser { get; set; }
        public DbSet<IMS_InviteCode> IMS_InviteCode { get; set; }
        public DbSet<IMS_InviteCodeRequest> IMS_InviteCodeRequest { get; set; }
        public DbSet<IMS_SalesCode> IMS_SalesCode { get; set; }
        public DbSet<IMS_SectionBrand> IMS_SectionBrand { get; set; }
        public DbSet<IMS_SectionOperator> IMS_SectionOperator { get; set; }
        public DbSet<IMS_Tag> IMS_Tag { get; set; }
        public DbSet<InboundPackage> InboundPackages { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<JobSuccessHistory> JobSuccessHistories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Map4Brand> Map4Brand { get; set; }
        public DbSet<Map4Category> Map4Category { get; set; }
        public DbSet<Map4Inventory> Map4Inventory { get; set; }
        public DbSet<Map4Order> Map4Order { get; set; }
        public DbSet<Map4Product> Map4Product { get; set; }
        public DbSet<MappedProductBackup> MappedProductBackups { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }
        public DbSet<OPC_AuthMenu> OPC_AuthMenu { get; set; }
        public DbSet<OPC_AuthRole> OPC_AuthRole { get; set; }
        public DbSet<OPC_AuthRoleMenu> OPC_AuthRoleMenu { get; set; }
        public DbSet<OPC_AuthRoleUser> OPC_AuthRoleUser { get; set; }
        public DbSet<OPC_AuthUser> OPC_AuthUser { get; set; }
        public DbSet<OPC_CategoryMap> OPC_CategoryMap { get; set; }
        public DbSet<OPC_ChannelMap> OPC_ChannelMap { get; set; }
        public DbSet<OPC_ChannelProduct> OPC_ChannelProduct { get; set; }
        public DbSet<OPC_OrderComment> OPC_OrderComment { get; set; }
        public DbSet<OPC_OrderSplitLog> OPC_OrderSplitLog { get; set; }
        public DbSet<OPC_OrgInfo> OPC_OrgInfo { get; set; }
        public DbSet<OPC_RMA> OPC_RMA { get; set; }
        public DbSet<OPC_RMAComment> OPC_RMAComment { get; set; }
        public DbSet<OPC_RMADetail> OPC_RMADetail { get; set; }
        public DbSet<OPC_RMALog> OPC_RMALog { get; set; }
        public DbSet<OPC_RMANotificationLog> OPC_RMANotificationLog { get; set; }
        public DbSet<OPC_Sale> OPC_Sale { get; set; }
        public DbSet<OPC_SaleComment> OPC_SaleComment { get; set; }
        public DbSet<OPC_SaleDetail> OPC_SaleDetail { get; set; }
        public DbSet<OPC_SaleLog> OPC_SaleLog { get; set; }
        public DbSet<OPC_SaleOrderNotificationLog> OPC_SaleOrderNotificationLog { get; set; }
        public DbSet<OPC_SaleRMA> OPC_SaleRMA { get; set; }
        public DbSet<OPC_SaleRMAComment> OPC_SaleRMAComment { get; set; }
        public DbSet<OPC_ShippingSale> OPC_ShippingSale { get; set; }
        public DbSet<OPC_ShippingSaleComment> OPC_ShippingSaleComment { get; set; }
        public DbSet<OPC_SKU> OPC_SKU { get; set; }
        public DbSet<OPC_Stock> OPC_Stock { get; set; }
        public DbSet<OPC_StockPropertyValueRaw> OPC_StockPropertyValueRaw { get; set; }
        public DbSet<OPC_StorePriority> OPC_StorePriority { get; set; }
        public DbSet<OPC_SupplierInfo> OPC_SupplierInfo { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order2Ex> Order2Ex { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }
        public DbSet<OrderTransaction> OrderTransactions { get; set; }
        public DbSet<Outbound> Outbounds { get; set; }
        public DbSet<OutboundItem> OutboundItems { get; set; }
        public DbSet<OutsiteUser> OutsiteUsers { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentNotifyLog> PaymentNotifyLogs { get; set; }
        public DbSet<PKey> PKeys { get; set; }
        public DbSet<PMessage> PMessages { get; set; }
        public DbSet<PointHistory> PointHistories { get; set; }
        public DbSet<PointOrderRule> PointOrderRules { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product2IMSTag> Product2IMSTag { get; set; }
        public DbSet<ProductCode2StoreCode> ProductCode2StoreCode { get; set; }
        public DbSet<ProductMap> ProductMaps { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<ProductPropertyStage> ProductPropertyStages { get; set; }
        public DbSet<ProductPropertyValue> ProductPropertyValues { get; set; }
        public DbSet<ProductStage> ProductStages { get; set; }
        public DbSet<ProductUploadJob> ProductUploadJobs { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Promotion2Product> Promotion2Product { get; set; }
        public DbSet<PromotionBrandRelation> PromotionBrandRelations { get; set; }
        public DbSet<Remind> Reminds { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceStage> ResourceStages { get; set; }
        public DbSet<RMA> RMAs { get; set; }
        public DbSet<RMA2Ex> RMA2Ex { get; set; }
        public DbSet<RMAItem> RMAItems { get; set; }
        public DbSet<RMALog> RMALogs { get; set; }
        public DbSet<RMAReason> RMAReasons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleAccessRight> RoleAccessRights { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SectionBrandImportStage> SectionBrandImportStages { get; set; }
        public DbSet<SectionBrandImportStageOutput> SectionBrandImportStageOutputs { get; set; }
        public DbSet<Seed> Seeds { get; set; }
        public DbSet<ShareHistory> ShareHistories { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<ShipVia> ShipVias { get; set; }
        public DbSet<SpecialTopic> SpecialTopics { get; set; }
        public DbSet<SpecialTopicProductRelation> SpecialTopicProductRelations { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreCoupon> StoreCoupons { get; set; }
        public DbSet<StorePromotion> StorePromotions { get; set; }
        public DbSet<StorePromotionScope> StorePromotionScopes { get; set; }
        public DbSet<StoreReal> StoreReals { get; set; }
        public DbSet<Supplier_Brand> Supplier_Brand { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TimeSeed> TimeSeeds { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserAuth> UserAuths { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<VerifyCode> VerifyCodes { get; set; }
        public DbSet<WXReply> WXReplies { get; set; }
        public DbSet<VUser> VUsers { get; set; }
        public DbSet<VUserRole> VUserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdminAccessRightMap());
            modelBuilder.Configurations.Add(new BannerMap());
            modelBuilder.Configurations.Add(new BrandMap());
            modelBuilder.Configurations.Add(new CardMap());
            modelBuilder.Configurations.Add(new CardBlackMap());
            modelBuilder.Configurations.Add(new CategoryMap());

            modelBuilder.Configurations.Add(new CategoryPropertyMap());
            modelBuilder.Configurations.Add(new CategoryPropertyValueMap());
            modelBuilder.Configurations.Add(new ChannelMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new ConfigMsgMap());
            modelBuilder.Configurations.Add(new CouponHistoryMap());
            modelBuilder.Configurations.Add(new CouponLogMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new DeviceLogMap());
            modelBuilder.Configurations.Add(new DeviceTokenMap());
            modelBuilder.Configurations.Add(new ExOrderMap());
            modelBuilder.Configurations.Add(new FavoriteMap());
            modelBuilder.Configurations.Add(new FeedbackMap());
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new HotWordMap());
            modelBuilder.Configurations.Add(new IMS_AssociateMap());
            modelBuilder.Configurations.Add(new IMS_AssociateBrandMap());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeMap());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeHistoryMap());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeRequestMap());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeRuleMap());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeRuleFixMap());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeRuleFlattenMap());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeRuleFlexMap());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeTran2ReqMap());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeTransferMap());
            modelBuilder.Configurations.Add(new IMS_AssociateItemsMap());
            modelBuilder.Configurations.Add(new IMS_AssociateSaleCodeMap());
            modelBuilder.Configurations.Add(new IMS_BankMap());
            modelBuilder.Configurations.Add(new IMS_ComboMap());
            modelBuilder.Configurations.Add(new IMS_Combo2ProductMap());
            modelBuilder.Configurations.Add(new IMS_GiftCardMap());
            modelBuilder.Configurations.Add(new IMS_GiftCardItemMap());
            modelBuilder.Configurations.Add(new IMS_GiftCardOrderMap());
            modelBuilder.Configurations.Add(new IMS_GiftCardRechargeMap());
            modelBuilder.Configurations.Add(new IMS_GiftCardTransfersMap());
            modelBuilder.Configurations.Add(new IMS_GiftCardUserMap());
            modelBuilder.Configurations.Add(new IMS_InviteCodeMap());
            modelBuilder.Configurations.Add(new IMS_InviteCodeRequestMap());
            modelBuilder.Configurations.Add(new IMS_SalesCodeMap());
            modelBuilder.Configurations.Add(new IMS_SectionBrandMap());
            modelBuilder.Configurations.Add(new IMS_SectionOperatorMap());
            modelBuilder.Configurations.Add(new IMS_TagMap());
            modelBuilder.Configurations.Add(new InboundPackageMap());
            modelBuilder.Configurations.Add(new InventoryMap());
            modelBuilder.Configurations.Add(new JobSuccessHistoryMap());
            modelBuilder.Configurations.Add(new LikeMap());
            modelBuilder.Configurations.Add(new Map4BrandMap());
            modelBuilder.Configurations.Add(new Map4CategoryMap());
            modelBuilder.Configurations.Add(new Map4InventoryMap());
            modelBuilder.Configurations.Add(new Map4OrderMap());
            modelBuilder.Configurations.Add(new Map4ProductMap());
            modelBuilder.Configurations.Add(new MappedProductBackupMap());
            modelBuilder.Configurations.Add(new NotificationLogMap());
            modelBuilder.Configurations.Add(new OPC_AuthMenuMap());
            modelBuilder.Configurations.Add(new OPC_AuthRoleMap());
            modelBuilder.Configurations.Add(new OPC_AuthRoleMenuMap());
            modelBuilder.Configurations.Add(new OPC_AuthRoleUserMap());
            modelBuilder.Configurations.Add(new OPC_AuthUserMap());
            modelBuilder.Configurations.Add(new OPC_CategoryMapMap());
            modelBuilder.Configurations.Add(new OPC_ChannelMapMap());
            modelBuilder.Configurations.Add(new OPC_ChannelProductMap());
            modelBuilder.Configurations.Add(new OPC_OrderCommentMap());
            modelBuilder.Configurations.Add(new OPC_OrderSplitLogMap());
            modelBuilder.Configurations.Add(new OPC_OrgInfoMap());
            modelBuilder.Configurations.Add(new OPC_RMAMap());
            modelBuilder.Configurations.Add(new OPC_RMACommentMap());
            modelBuilder.Configurations.Add(new OPC_RMADetailMap());
            modelBuilder.Configurations.Add(new OPC_RMALogMap());
            modelBuilder.Configurations.Add(new OPC_RMANotificationLogMap());
            modelBuilder.Configurations.Add(new OPC_SaleMap());
            modelBuilder.Configurations.Add(new OPC_SaleCommentMap());
            modelBuilder.Configurations.Add(new OPC_SaleDetailMap());
            modelBuilder.Configurations.Add(new OPC_SaleLogMap());
            modelBuilder.Configurations.Add(new OPC_SaleOrderNotificationLogMap());
            modelBuilder.Configurations.Add(new OPC_SaleRMAMap());
            modelBuilder.Configurations.Add(new OPC_SaleRMACommentMap());
            modelBuilder.Configurations.Add(new OPC_ShippingSaleMap());
            modelBuilder.Configurations.Add(new OPC_ShippingSaleCommentMap());
            modelBuilder.Configurations.Add(new OPC_SKUMap());
            modelBuilder.Configurations.Add(new OPC_StockMap());
            modelBuilder.Configurations.Add(new OPC_StockPropertyValueRawMap());
            modelBuilder.Configurations.Add(new OPC_StorePriorityMap());
            modelBuilder.Configurations.Add(new OPC_SupplierInfoMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new Order2ExMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
            modelBuilder.Configurations.Add(new OrderLogMap());
            modelBuilder.Configurations.Add(new OrderTransactionMap());
            modelBuilder.Configurations.Add(new OutboundMap());
            modelBuilder.Configurations.Add(new OutboundItemMap());
            modelBuilder.Configurations.Add(new OutsiteUserMap());
            modelBuilder.Configurations.Add(new PaymentMethodMap());
            modelBuilder.Configurations.Add(new PaymentNotifyLogMap());
            modelBuilder.Configurations.Add(new PKeyMap());
            modelBuilder.Configurations.Add(new PMessageMap());
            modelBuilder.Configurations.Add(new PointHistoryMap());
            modelBuilder.Configurations.Add(new PointOrderRuleMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new Product2IMSTagMap());
            modelBuilder.Configurations.Add(new ProductCode2StoreCodeMap());

            modelBuilder.Configurations.Add(new ProductPropertyMap());
            modelBuilder.Configurations.Add(new ProductPropertyStageMap());
            modelBuilder.Configurations.Add(new ProductPropertyValueMap());
            modelBuilder.Configurations.Add(new ProductStageMap());
            modelBuilder.Configurations.Add(new ProductUploadJobMap());
            modelBuilder.Configurations.Add(new PromotionMap());
            modelBuilder.Configurations.Add(new Promotion2ProductMap());
            modelBuilder.Configurations.Add(new PromotionBrandRelationMap());
            modelBuilder.Configurations.Add(new RemindMap());
            modelBuilder.Configurations.Add(new ResourceMap());
            modelBuilder.Configurations.Add(new ResourceStageMap());
            modelBuilder.Configurations.Add(new RMAMap());
            modelBuilder.Configurations.Add(new RMA2ExMap());
            modelBuilder.Configurations.Add(new RMAItemMap());
            modelBuilder.Configurations.Add(new RMALogMap());
            modelBuilder.Configurations.Add(new RMAReasonMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new RoleAccessRightMap());
            modelBuilder.Configurations.Add(new SectionMap());
            modelBuilder.Configurations.Add(new SectionBrandImportStageMap());
            modelBuilder.Configurations.Add(new SectionBrandImportStageOutputMap());
            modelBuilder.Configurations.Add(new SeedMap());
            modelBuilder.Configurations.Add(new ShareHistoryMap());
            modelBuilder.Configurations.Add(new ShippingAddressMap());
            modelBuilder.Configurations.Add(new ShipViaMap());
            modelBuilder.Configurations.Add(new SpecialTopicMap());
            modelBuilder.Configurations.Add(new SpecialTopicProductRelationMap());
            modelBuilder.Configurations.Add(new StoreMap());
            modelBuilder.Configurations.Add(new StoreCouponMap());
            modelBuilder.Configurations.Add(new StorePromotionMap());
            modelBuilder.Configurations.Add(new StorePromotionScopeMap());
            modelBuilder.Configurations.Add(new StoreRealMap());
            modelBuilder.Configurations.Add(new Supplier_BrandMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new TimeSeedMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserAccountMap());
            modelBuilder.Configurations.Add(new UserAuthMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new VerifyCodeMap());
            modelBuilder.Configurations.Add(new WXReplyMap());
            modelBuilder.Configurations.Add(new VUserMap());
            modelBuilder.Configurations.Add(new VUserRoleMap());
        }
    }
}
