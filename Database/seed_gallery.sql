-- Seeds the portfolio gallery (BussinessGallary) with 22 additional showcase units
-- so the "معرض أعمالنا" section on the homepage isn't nearly empty.
-- Safe to re-run: does nothing if it has already been applied (checks for image-026.jpg).
--
-- Usage (adjust server/database name as needed):
--   sqlcmd -S <server> -d <database> -U <user> -P <password> -C -f 65001 -i Database\seed_gallery.sql
-- or run it from SSMS / Azure Data Studio / your host's SQL query tool.
--
-- Prerequisite: the 22 image files this script references (image-002.jpg, image-004.jpg,
-- image-005.jpg, image-006.jpg, image-007.jpg .. image-017.jpg, image-019.jpg, image-021.jpg,
-- image-022.jpg .. image-026.jpg) must exist under wwwroot/ImageFinshProject/Image on the
-- target host. They're already committed to the repo, so a normal deploy carries them.

SET NOCOUNT ON;

IF NOT EXISTS (SELECT 1 FROM BussinessGallary WHERE ProfileImage = N'image-026.jpg')
BEGIN
    INSERT INTO BussinessGallary
        (ServicesName, City, Engineer, Price, Vendor, BussinessDate, DetailsUnit, InteriorDesign, FinishingQuality, LinkVideo, ProfileImage, Filter, AreaWide)
    VALUES
    (N'لوكس', N'التجمع الأول', N'مهندس أحمد سامي', 380000, N'Ramco', '2023-11-05', N'استقبال فاخر بإضاءة كريستال وأثاث مخملي يجمع بين الدفء والأناقة.', N'ألوان بيج ونيود مع تفاصيل خشبية دافئة', N'دهانات ايطالية وأرضيات بورسلين لامع', NULL, N'image-002.jpg', N'filter-reception', 24),

    (N'سوبر لوكس', N'الشيخ زايد', N'مهندسة ياسمين خالد', 1250000, N'Palm Hills', '2024-01-10', N'ريسبشن مفتوح يجمع بين الصالة والسفرة بتصميم عصري مريح.', N'تصميم مفتوح بخطوط عصرية ومساحات معيشة واسعة', N'تشطيب سوبر لوكس بخامات مستوردة', NULL, N'image-004.jpg', N'filter-living', 45),

    (N'سوبر لوكس', N'مدينتي', N'مهندس محمود عادل', 1380000, N'Mountain View', '2024-01-18', N'صالة معيشة أنيقة بإطلالة على الحديقة الداخلية.', N'ديكور هادئ بدرجات الرمادي والبني', N'أرضيات باركيه طبيعي ودهانات صديقة للبيئة', NULL, N'image-005.jpg', N'filter-living', 42),

    (N'الترا سوبر لوكس', N'العاصمة الإدارية', N'مهندس عمرو مصطفى', 2650000, N'Ora', '2024-02-02', N'صالة استقبال بتصميم فندقي وتفاصيل ذهبية فاخرة.', N'لمسات ذهبية وأثاث إيطالي فاخر', N'رخام طبيعي وأسقف مستعارة بإضاءة مخفية', NULL, N'image-006.jpg', N'filter-living', 52),

    (N'سوبر لوكس', N'التجمع الخامس', N'مهندسة نور الشريف', 620000, N'Sodic', '2023-12-14', N'غرفة نوم مريحة بتصميم عملي يناسب جميع الأعمار.', N'درجات ترابية دافئة مع دولاب حائط مدمج', N'أرضيات لامينت وطلاء مقاوم للرطوبة', NULL, N'image-007.jpg', N'filter-rooms', 18),

    (N'سوبر لوكس', N'الرحاب', N'مهندس كريم فوزي', 640000, N'La Vista', '2023-12-20', N'غرفة نوم ثانوية بإضاءة طبيعية وتخزين واسع.', N'ألوان هادئة ومساحات تخزين ذكية', N'دهانات مطفية ودواليب خشب MDF', NULL, N'image-008.jpg', N'filter-rooms', 16),

    (N'الترا سوبر لوكس', N'6 أكتوبر', N'مهندسة سارة حبيل', 980000, N'Hyde Park', '2024-02-15', N'غرفة نوم بتصميم عصري وألوان جريئة.', N'تباين بين الرمادي الغامق والأبيض', N'أرضيات خشبية وإضاءة مخفية بالسقف', NULL, N'image-009.jpg', N'filter-rooms', 20),

    (N'لوكس', N'التجمع الأول', N'مهندس أحمد سامي', 410000, N'Ramco', '2023-11-22', N'غرفة نوم اقتصادية بتصميم عملي ومريح.', N'تصميم بسيط بألوان فاتحة', N'دهانات بلاستيك ودواليب جاهزة', NULL, N'image-010.jpg', N'filter-rooms', 14),

    (N'سوبر لوكس', N'الشيخ زايد', N'مهندسة ياسمين خالد', 690000, N'Palm Hills', '2024-01-25', N'غرفة نوم بإطلالة حديقة وتصميم عائلي دافئ.', N'درجات بيج مع تفاصيل خشبية', N'أرضيات سيراميك خشبي', NULL, N'image-011.jpg', N'filter-rooms', 19),

    (N'الترا سوبر لوكس', N'مدينتي', N'مهندس محمود عادل', 1050000, N'Mountain View', '2024-02-20', N'غرفة نوم رئيسية ملحقة بغرفة ملابس صغيرة.', N'تصميم فندقي بلمسات فاخرة', N'رخام وأرضيات باركيه مستورد', NULL, N'image-012.jpg', N'filter-rooms', 24),

    (N'سوبر لوكس', N'العاصمة الإدارية', N'مهندس عمرو مصطفى', 700000, N'Ora', '2024-03-01', N'غرفة أطفال بتصميم مبهج وآمن.', N'ألوان زاهية وتخزين عملي', N'دهانات صحية خالية من الرصاص', NULL, N'image-013.jpg', N'filter-rooms', 15),

    (N'الترا سوبر لوكس', N'التجمع الخامس', N'مهندسة نور الشريف', 1450000, N'Sodic', '2024-03-08', N'غرفة نوم رئيسية فاخرة بتصميم خشبي دافئ وتكييف مركزي.', N'خشب طبيعي وإضاءة مخفية بالسقف المعلق', N'أرضيات باركيه وستائر مخصصة', NULL, N'image-014.jpg', N'filter-master', 28),

    (N'الترا سوبر لوكس', N'الرحاب', N'مهندس كريم فوزي', 1520000, N'La Vista', '2024-03-15', N'ماستر روم بمساحة واسعة ومنطقة جلوس خاصة.', N'درجات نيود مع تفاصيل نحاسية', N'رخام وأرضيات خشبية مستوردة', NULL, N'image-015.jpg', N'filter-master', 30),

    (N'سوبر لوكس', N'6 أكتوبر', N'مهندسة سارة حبيل', 980000, N'Hyde Park', '2024-03-22', N'غرفة نوم رئيسية بإطلالة بانورامية.', N'تصميم عصري بدرجات رمادية', N'أرضيات لامينت فاخر', NULL, N'image-016.jpg', N'filter-master', 26),

    (N'الترا سوبر لوكس', N'التجمع الأول', N'مهندس أحمد سامي', 1680000, N'Ramco', '2024-04-02', N'جناح رئيسي متكامل بحمام ودريسنج ملحقين.', N'لمسات فاخرة وأثاث مصمم خصيصًا', N'رخام طبيعي وتشطيبات يدوية دقيقة', NULL, N'image-017.jpg', N'filter-master', 34),

    (N'سوبر لوكس', N'الشيخ زايد', N'مهندسة ياسمين خالد', 320000, N'Palm Hills', '2024-01-30', N'غرفة ملابس عملية بتقسيمات ذكية ومرايا مدمجة.', N'دواليب مفتوحة بإضاءة داخلية', N'خامات خشبية مقاومة للخدش', NULL, N'image-019.jpg', N'filter-dressing', 10),

    (N'الترا سوبر لوكس', N'مدينتي', N'مهندس محمود عادل', 280000, N'Mountain View', '2024-02-10', N'حمام فندقي بتصميم زجاجي وإضاءة دافئة.', N'رخام وسيراميك بلمسة معدنية', N'أدوات صحية أوروبية الصنع', NULL, N'image-021.jpg', N'filter-toilet', 8),

    (N'سوبر لوكس', N'العاصمة الإدارية', N'مهندس عمرو مصطفى', 450000, N'Ora', '2024-02-25', N'مطبخ مفتوح بتصميم عملي وتخزين واسع.', N'وحدات خشبية بدرجتين متباينتين', N'رخام صناعي مقاوم للحرارة', NULL, N'image-022.jpg', N'filter-kitchen', 16),

    (N'الترا سوبر لوكس', N'التجمع الخامس', N'مهندسة نور الشريف', 580000, N'Sodic', '2024-03-05', N'مطبخ فاخر بجزيرة وسطية وأجهزة مدمجة.', N'تصميم إيطالي بخطوط نظيفة', N'رخام طبيعي وأجهزة بلت إن', NULL, N'image-023.jpg', N'filter-kitchen', 20),

    (N'لوكس', N'الرحاب', N'مهندس كريم فوزي', 260000, N'La Vista', '2024-01-12', N'مطبخ اقتصادي عملي بتصميم بسيط وأنيق.', N'وحدات ميلامين عالية الجودة', N'كاونتر جرانيت وحوض ستانلس', NULL, N'image-024.jpg', N'filter-kitchen', 12),

    (N'سوبر لوكس', N'6 أكتوبر', N'مهندسة سارة حبيل', 210000, N'Hyde Park', '2024-03-18', N'تراس خارجي مجهز للجلسات العائلية.', N'أرضيات خشبية خارجية ونباتات طبيعية', N'دهانات مقاومة للعوامل الجوية', NULL, N'image-025.jpg', N'filter-terrace', 22),

    (N'الترا سوبر لوكس', N'التجمع الأول', N'مهندس أحمد سامي', 340000, N'Ramco', '2024-04-10', N'روف تراس بإطلالة بانورامية ومنطقة شواء.', N'تصميم خارجي فاخر بإضاءة ليلية', N'بورسلين خارجي مقاوم للانزلاق', NULL, N'image-026.jpg', N'filter-terrace', 35);

    PRINT 'Inserted 22 gallery rows.';
END
ELSE
BEGIN
    PRINT 'Seed already applied - skipped.';
END

SELECT COUNT(*) AS TotalRows FROM BussinessGallary;
