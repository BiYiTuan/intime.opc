using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Intime.OPC.Domain.Models.Mapping;

namespace Intime.OPC.Domain.Models
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
        public DbSet<Map4Store> Map4Store { get; set; }
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
            modelBuilder.Configurations.Add(new AdminAccessRightMapper());
            modelBuilder.Configurations.Add(new BannerMapper());
            modelBuilder.Configurations.Add(new BrandMapper());
            modelBuilder.Configurations.Add(new CardMapper());
            modelBuilder.Configurations.Add(new CardBlackMapper());
            modelBuilder.Configurations.Add(new CategoryMapper());
            modelBuilder.Configurations.Add(new CategoryMapMapper());
            modelBuilder.Configurations.Add(new CategoryPropertyMapper());
            modelBuilder.Configurations.Add(new CategoryPropertyValueMapper());
            modelBuilder.Configurations.Add(new ChannelMapper());
            modelBuilder.Configurations.Add(new CityMapper());
            modelBuilder.Configurations.Add(new CommentMapper());
            modelBuilder.Configurations.Add(new ConfigMsgMapper());
            modelBuilder.Configurations.Add(new CouponHistoryMapper());
            modelBuilder.Configurations.Add(new CouponLogMapper());
            modelBuilder.Configurations.Add(new DepartmentMapper());
            modelBuilder.Configurations.Add(new DeviceLogMapper());
            modelBuilder.Configurations.Add(new DeviceTokenMapper());
            modelBuilder.Configurations.Add(new ExOrderMapper());
            modelBuilder.Configurations.Add(new FavoriteMapper());
            modelBuilder.Configurations.Add(new FeedbackMapper());
            modelBuilder.Configurations.Add(new GroupMapper());
            modelBuilder.Configurations.Add(new HotWordMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateBrandMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeHistoryMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeRequestMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeRuleMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeRuleFixMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeRuleFlattenMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeRuleFlexMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeTran2ReqMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateIncomeTransferMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateItemsMapper());
            modelBuilder.Configurations.Add(new IMS_AssociateSaleCodeMapper());
            modelBuilder.Configurations.Add(new IMS_BankMapper());
            modelBuilder.Configurations.Add(new IMS_ComboMapper());
            modelBuilder.Configurations.Add(new IMS_Combo2ProductMapper());
            modelBuilder.Configurations.Add(new IMS_GiftCardMapper());
            modelBuilder.Configurations.Add(new IMS_GiftCardItemMapper());
            modelBuilder.Configurations.Add(new IMS_GiftCardOrderMapper());
            modelBuilder.Configurations.Add(new IMS_GiftCardRechargeMapper());
            modelBuilder.Configurations.Add(new IMS_GiftCardTransfersMapper());
            modelBuilder.Configurations.Add(new IMS_GiftCardUserMapper());
            modelBuilder.Configurations.Add(new IMS_InviteCodeMapper());
            modelBuilder.Configurations.Add(new IMS_InviteCodeRequestMapper());
            modelBuilder.Configurations.Add(new IMS_SalesCodeMapper());
            modelBuilder.Configurations.Add(new IMS_SectionBrandMapper());
            modelBuilder.Configurations.Add(new IMS_SectionOperatorMapper());
            modelBuilder.Configurations.Add(new IMS_TagMapper());
            modelBuilder.Configurations.Add(new InboundPackageMapper());
            modelBuilder.Configurations.Add(new InventoryMapper());
            modelBuilder.Configurations.Add(new JobSuccessHistoryMapper());
            modelBuilder.Configurations.Add(new LikeMapper());
            modelBuilder.Configurations.Add(new Map4BrandMapper());
            modelBuilder.Configurations.Add(new Map4CategoryMapper());
            modelBuilder.Configurations.Add(new Map4InventoryMapper());
            modelBuilder.Configurations.Add(new Map4OrderMapper());
            modelBuilder.Configurations.Add(new Map4ProductMapper());
            modelBuilder.Configurations.Add(new Map4StoreMapper());
            modelBuilder.Configurations.Add(new MappedProductBackupMapper());
            modelBuilder.Configurations.Add(new NotificationLogMapper());
            modelBuilder.Configurations.Add(new OPC_AuthMenuMapper());
            modelBuilder.Configurations.Add(new OPC_AuthRoleMapper());
            modelBuilder.Configurations.Add(new OPC_AuthRoleMenuMapper());
            modelBuilder.Configurations.Add(new OPC_AuthRoleUserMapper());
            modelBuilder.Configurations.Add(new OPC_AuthUserMapper());
            modelBuilder.Configurations.Add(new OPC_CategoryMapMapper());
            modelBuilder.Configurations.Add(new OPC_ChannelMapMapper());
            modelBuilder.Configurations.Add(new OPC_ChannelProductMapper());
            modelBuilder.Configurations.Add(new OPC_OrderCommentMapper());
            modelBuilder.Configurations.Add(new OPC_OrderSplitLogMapper());
            modelBuilder.Configurations.Add(new OPC_OrgInfoMapper());
            modelBuilder.Configurations.Add(new OPC_RMAMapper());
            modelBuilder.Configurations.Add(new OPC_RMACommentMapper());
            modelBuilder.Configurations.Add(new OPC_RMADetailMapper());
            modelBuilder.Configurations.Add(new OPC_RMALogMapper());
            modelBuilder.Configurations.Add(new OPC_RMANotificationLogMapper());
            modelBuilder.Configurations.Add(new OPC_SaleMapper());
            modelBuilder.Configurations.Add(new OPC_SaleCommentMapper());
            modelBuilder.Configurations.Add(new OPC_SaleDetailMapper());
            modelBuilder.Configurations.Add(new OPC_SaleLogMapper());
            modelBuilder.Configurations.Add(new OPC_SaleOrderNotificationLogMapper());
            modelBuilder.Configurations.Add(new OPC_SaleRMAMapper());
            modelBuilder.Configurations.Add(new OPC_SaleRMACommentMapper());
            modelBuilder.Configurations.Add(new OPC_ShippingSaleMapper());
            modelBuilder.Configurations.Add(new OPC_ShippingSaleCommentMapper());
            modelBuilder.Configurations.Add(new OPC_SKUMapper());
            modelBuilder.Configurations.Add(new OPC_StockMapper());
            modelBuilder.Configurations.Add(new OPC_StockPropertyValueRawMapper());
            modelBuilder.Configurations.Add(new OPC_StorePriorityMapper());
            modelBuilder.Configurations.Add(new OPC_SupplierInfoMapper());
            modelBuilder.Configurations.Add(new OrderMapper());
            modelBuilder.Configurations.Add(new Order2ExMapper());
            modelBuilder.Configurations.Add(new OrderItemMapper());
            modelBuilder.Configurations.Add(new OrderLogMapper());
            modelBuilder.Configurations.Add(new OrderTransactionMapper());
            modelBuilder.Configurations.Add(new OutboundMapper());
            modelBuilder.Configurations.Add(new OutboundItemMapper());
            modelBuilder.Configurations.Add(new OutsiteUserMapper());
            modelBuilder.Configurations.Add(new PaymentMethodMapper());
            modelBuilder.Configurations.Add(new PaymentNotifyLogMapper());
            modelBuilder.Configurations.Add(new PKeyMapper());
            modelBuilder.Configurations.Add(new PMessageMapper());
            modelBuilder.Configurations.Add(new PointHistoryMapper());
            modelBuilder.Configurations.Add(new PointOrderRuleMapper());
            modelBuilder.Configurations.Add(new ProductMapper());
            modelBuilder.Configurations.Add(new Product2IMSTagMapper());
            modelBuilder.Configurations.Add(new ProductCode2StoreCodeMapper());
            modelBuilder.Configurations.Add(new ProductMapMapper());
            modelBuilder.Configurations.Add(new ProductPropertyMapper());
            modelBuilder.Configurations.Add(new ProductPropertyStageMapper());
            modelBuilder.Configurations.Add(new ProductPropertyValueMapper());
            modelBuilder.Configurations.Add(new ProductStageMapper());
            modelBuilder.Configurations.Add(new ProductUploadJobMapper());
            modelBuilder.Configurations.Add(new PromotionMapper());
            modelBuilder.Configurations.Add(new Promotion2ProductMapper());
            modelBuilder.Configurations.Add(new PromotionBrandRelationMapper());
            modelBuilder.Configurations.Add(new RemindMapper());
            modelBuilder.Configurations.Add(new ResourceMapper());
            modelBuilder.Configurations.Add(new ResourceStageMapper());
            modelBuilder.Configurations.Add(new RMAMapper());
            modelBuilder.Configurations.Add(new RMA2ExMapper());
            modelBuilder.Configurations.Add(new RMAItemMapper());
            modelBuilder.Configurations.Add(new RMALogMapper());
            modelBuilder.Configurations.Add(new RMAReasonMapper());
            modelBuilder.Configurations.Add(new RoleMapper());
            modelBuilder.Configurations.Add(new RoleAccessRightMapper());
            modelBuilder.Configurations.Add(new SectionMapper());
            modelBuilder.Configurations.Add(new SectionBrandImportStageMapper());
            modelBuilder.Configurations.Add(new SectionBrandImportStageOutputMapper());
            modelBuilder.Configurations.Add(new SeedMapper());
            modelBuilder.Configurations.Add(new ShareHistoryMapper());
            modelBuilder.Configurations.Add(new ShippingAddressMapper());
            modelBuilder.Configurations.Add(new ShipViaMapper());
            modelBuilder.Configurations.Add(new SpecialTopicMapper());
            modelBuilder.Configurations.Add(new SpecialTopicProductRelationMapper());
            modelBuilder.Configurations.Add(new StoreMapper());
            modelBuilder.Configurations.Add(new StoreCouponMapper());
            modelBuilder.Configurations.Add(new StorePromotionMapper());
            modelBuilder.Configurations.Add(new StorePromotionScopeMapper());
            modelBuilder.Configurations.Add(new StoreRealMapper());
            modelBuilder.Configurations.Add(new Supplier_BrandMapper());
            modelBuilder.Configurations.Add(new TagMapper());
            modelBuilder.Configurations.Add(new TimeSeedMapper());
            modelBuilder.Configurations.Add(new UserMapper());
            modelBuilder.Configurations.Add(new UserAccountMapper());
            modelBuilder.Configurations.Add(new UserAuthMapper());
            modelBuilder.Configurations.Add(new UserRoleMapper());
            modelBuilder.Configurations.Add(new VerifyCodeMapper());
            modelBuilder.Configurations.Add(new WXReplyMapper());
            modelBuilder.Configurations.Add(new VUserMapper());
            modelBuilder.Configurations.Add(new VUserRoleMapper());
        }
    }
}
