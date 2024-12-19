--Create types of bed
DECLARE @PolytunnelTypeId INT;
DECLARE @OutdoorTypeId INT;
INSERT INTO BlockTypes (TypeName)
VALUES ('Polytunnel'), ('Outdoor');

SET @PolytunnelTypeId = (SELECT BlockTypeId FROM BlockTypes WHERE TypeName = 'Polytunnel');
SET @OutdoorTypeId = (SELECT BlockTypeId FROM BlockTypes WHERE TypeName = 'Outdoor');

-- Insert blocks
DECLARE @PolytunnelBlockId INT;
DECLARE @GardenBlock1Id INT;
DECLARE @GardenBlock2Id INT;
DECLARE @GardenBlock3Id INT;
DECLARE @GardenBlock4Id INT;
INSERT INTO Blocks (Name, BlockTypeId)
VALUES ('1 Polytunnel', @PolytunnelTypeId),
       ('1 Garden Block', @OutdoorTypeId),
       ('2 Garden Block', @OutdoorTypeId),
       ('3 Garden Block', @OutdoorTypeId),
       ('4 Garden Block', @OutdoorTypeId);

-- Retrieve Block IDs using SELECT
SET @PolytunnelBlockId = (SELECT BlockId FROM Blocks WHERE Name = '1 Polytunnel');
SET @GardenBlock1Id = (SELECT BlockId FROM Blocks WHERE Name = '1 Garden Block');
SET @GardenBlock2Id = (SELECT BlockId FROM Blocks WHERE Name = '2 Garden Block');
SET @GardenBlock3Id = (SELECT BlockId FROM Blocks WHERE Name = '3 Garden Block');
SET @GardenBlock4Id = (SELECT BlockId FROM Blocks WHERE Name = '4 Garden Block');

DECLARE @PropTunnelId INT;
INSERT INTO PropagationTunnel (Name)
VALUES 
('Main Propagation Tunnel');
SET @PropTunnelId = (SELECT TunnelId FROM PropagationTunnel WHERE Name = 'Main Propagation Tunnel');

DECLARE @AreaHeatedTableId INT;
DECLARE @AreaUnheatedTableId INT;

INSERT INTO PropagationAreas (TunnelId, Name, MaxTrays)
VALUES 
(@PropTunnelId, 'Heated Table', 20),
(@PropTunnelId, 'Unheated Table', 30);

-- Retrieve Area IDs using SELECT
SET @AreaHeatedTableId = (SELECT AreaId FROM PropagationAreas WHERE Name = 'Heated Table');
SET @AreaUnheatedTableId = (SELECT AreaId FROM PropagationAreas WHERE Name = 'Unheated Table');


--Add tray types to be used in the propagation tunnel
DECLARE @ModuleTrayStandard60Id INT;
DECLARE @ModuleTrayLarge24Id INT;

INSERT INTO ModuleTrayTypes (Name, NumberOfModules)
VALUES 
('Standard 60-Cell Tray', 60),
('Large 24-Cell Tray', 24);

SET @ModuleTrayStandard60Id = (SELECT ModuleTrayTypes.TrayTypeId FROM ModuleTrayTypes WHERE Name = 'Standard 60-Cell Tray');
SET @ModuleTrayLarge24Id = (SELECT ModuleTrayTypes.TrayTypeId FROM ModuleTrayTypes WHERE Name = 'Large 24-Cell Tray');

INSERT INTO Beds (BlockId, Position, Length, Width)
VALUES 
--https://www.polytunnelsdirect.ie/product/polytunnel-package-fully-installed
(@PolytunnelBlockId, 1, 50, 4), (@PolytunnelBlockId, 2, 50, 4), (@PolytunnelBlockId, 3, 50, 4), -- Beds in Polytunnel Block 1, 60x18ft
(@GardenBlock1Id, 1, 50, 4), (@GardenBlock1Id, 2, 50, 4), (@GardenBlock1Id, 3, 50, 4), (@GardenBlock1Id, 4, 50, 4), -- Beds in Outdoor Block 1
(@GardenBlock2Id, 1, 50, 4), (@GardenBlock2Id, 2, 50, 4), (@GardenBlock2Id, 3, 50, 4), (@GardenBlock2Id, 4, 50, 4), -- Beds in Outdoor Block 2
(@GardenBlock3Id, 1, 50, 4), (@GardenBlock3Id, 2, 50, 4), (@GardenBlock3Id, 3, 50, 4), (@GardenBlock3Id, 4, 50, 4), -- Beds in Outdoor Block 3
(@GardenBlock4Id, 1, 50, 4), (@GardenBlock4Id, 2, 50, 4), (@GardenBlock4Id, 3, 50, 4), (@GardenBlock4Id, 4, 50, 4); -- Beds in Outdoor Block 4


-- Family of plants
DECLARE @FamilyAlliumTypeId INT;
DECLARE @FamilyApiaceaeTypeId INT;
DECLARE @FamilyAsteraceaeTypeId INT;
DECLARE @FamilySolanaceaeTypeId INT;
DECLARE @FamilyBrassicaceaeTypeId INT;
DECLARE @FamilyCucurbitaceaeTypeId INT;
DECLARE @FamilyFabaceaeTypeId INT;

INSERT INTO CropTypes (Family)
VALUES 
('Allium'),
('Apiaceae'),
('Asteraceae'),
('Solanaceae'),
('Brassicaceae'),
('Cucurbitaceae'),
('Fabaceae');


-- Retrieve CropType IDs using SELECT
SET @FamilyAlliumTypeId = (SELECT CropTypeId FROM CropTypes WHERE Family = 'Allium');
SET @FamilyApiaceaeTypeId = (SELECT CropTypeId FROM CropTypes WHERE Family = 'Apiaceae');
SET @FamilyAsteraceaeTypeId = (SELECT CropTypeId FROM CropTypes WHERE Family = 'Asteraceae');
SET @FamilySolanaceaeTypeId = (SELECT CropTypeId FROM CropTypes WHERE Family = 'Solanaceae');
SET @FamilyBrassicaceaeTypeId = (SELECT CropTypeId FROM CropTypes WHERE Family = 'Brassicaceae');
SET @FamilyCucurbitaceaeTypeId = (SELECT CropTypeId FROM CropTypes WHERE Family = 'Cucurbitaceae');
SET @FamilyFabaceaeTypeId = (SELECT CropTypeId FROM CropTypes WHERE Family = 'Fabaceae');


--Add crop details
DECLARE @SpringOnionCropId INT;

-- Insert into Crops and retrieve the newly inserted ID
INSERT INTO Crops (CropTypeId, Variety, IsDirectSow)
VALUES (
    @FamilyAlliumTypeId,
    'Spring Onion, White Lisbon', 
    0 -- Not Direct Sow
);

SET @SpringOnionCropId = SCOPE_IDENTITY();

-- Use the retrieved ID for CropBedAttributes
INSERT INTO CropBedAttributes (CropId, TimeToMaturity, RowSpacing, PlantSpacing, Notes)
VALUES (
    @SpringOnionCropId,
    60, -- Time to maturity in days
    10.00, -- Row spacing in cm
    3.00, -- Plant spacing in cm
    'Sow seeds 2-3cm apart in rows 10cm apart; no thinning needed. For early crops, start indoors in January.'
);

INSERT INTO CropPropagationAttributes (CropId, PropagationTime, GerminationTime, PreferredTemperature, Notes)
VALUES (
    @SpringOnionCropId,
    28, -- Propagation time in days (approx. 4 weeks)
    14, -- Germination time in days
    20.00, -- Preferred germination temperature in °C
    'For early crops, start indoors in February at 14°C. Direct sow from March to August.'
);

INSERT INTO Covers (CropId, CoverType, StartWeek, EndWeek, Notes)
VALUES (
    @SpringOnionCropId,
    'Fleece',
    10, -- Start week (early March)
    14, -- End week (early April)
    'Use fleece to protect early sowings from frost.'
);

-- **** ADD MORE CROPS HERE *****


--Where to plant
--@PolytunnelBlockId
DECLARE @PolytunnelBed1 INT;
DECLARE @PolytunnelBed2 INT;
DECLARE @PolytunnelBed3 INT;

SET @PolytunnelBed1 = (SELECT BedId FROM Beds WHERE BlockId = @PolytunnelBlockId AND Position = 1);
SET @PolytunnelBed2 = (SELECT BedId FROM Beds WHERE BlockId = @PolytunnelBlockId AND Position = 2);
SET @PolytunnelBed3 = (SELECT BedId FROM Beds WHERE BlockId = @PolytunnelBlockId AND Position = 3);

--@GardenBlock1Id
--@GardenBlock2Id
--@GardenBlock3Id
--@GardenBlock4Id

--GardenBlock1Id
-- Retrieve Bed IDs for GardenBlock1
DECLARE @GardenBlock1Bed1 INT;
DECLARE @GardenBlock1Bed2 INT;
DECLARE @GardenBlock1Bed3 INT;
DECLARE @GardenBlock1Bed4 INT;

SET @GardenBlock1Bed1 = (SELECT BedId FROM Beds WHERE BlockId = @GardenBlock1Id AND Position = 1);
SET @GardenBlock1Bed2 = (SELECT BedId FROM Beds WHERE BlockId = @GardenBlock1Id AND Position = 2);
SET @GardenBlock1Bed3 = (SELECT BedId FROM Beds WHERE BlockId = @GardenBlock1Id AND Position = 3);
SET @GardenBlock1Bed4 = (SELECT BedId FROM Beds WHERE BlockId = @GardenBlock1Id AND Position = 4);


--Spring onions
--Propagation tunnel

--Total number of trays, what is the calculation?
-- 4ft (120cm) bed 5ft(150cm) long.  Planting distances 10cm x 3cm 11 rows (leaving some space at the sides) x 450 columns = 4950 units
--tray with 60 units.  4950 / 60 = 83 trays
--multi sew 3 per unit 4950*3 = 19,800  Apparently 8 is possible

--tray 1, add 82 more????

--Calculate plating dates
DECLARE @GrowingDays INT;
DECLARE @BedPlantDate DATE;
DECLARE @BedRemovalDate DATE;
DECLARE @PropagationDays INT;
DECLARE @PropPlantDate DATE;
DECLARE @PropRemovalDate DATE;
DECLARE @SeedsPerUnit INT;

SET @SeedsPerUnit = 4; --Apparently 8 is possible
SET @BedRemovalDate = '2025-March-01';
SET @BedPlantDate = dateadd(dd, -@GrowingDays, @BedRemovalDate);
SET @GrowingDays = (SELECT TimeToMaturity FROM CropBedAttributes WHERE CropId=@SpringOnionCropId);
SET @PropagationDays = (SELECT PropagationTime FROM CropPropagationAttributes WHERE CropId=@SpringOnionCropId);
SET @PropRemovalDate = DateAdd(dd,-1,@BedPlantDate);
SET @PropPlantDate = DateAdd(dd,-@PropagationDays, @PropRemovalDate);

--Propagation
INSERT INTO ModuleTrays (AreaId, TrayTypeId, CropId, SeedsPerModule, PlantingDate, RemovalDate, BedCropId)
VALUES
(@AreaHeatedTableId, @ModuleTrayStandard60Id, @SpringOnionCropId, @SeedsPerUnit, @PropPlantDate, @PropRemovalDate, @GardenBlock1Bed1); -- spring onion

--Bed planting
INSERT INTO BedCrops (BedId, CropId, PlantingDate, RemovalDate)
VALUES
(@GardenBlock1Bed1, @SpringOnionCropId, @BedPlantDate, @BedRemovalDate); -- spring onion in Bed 1

--Second Planting, late spring (May)
SET @BedRemovalDate = '2025-July-20';
SET @BedPlantDate = dateadd(dd, -@GrowingDays, @BedRemovalDate);
SET @GrowingDays = (SELECT TimeToMaturity FROM CropBedAttributes WHERE CropId=@SpringOnionCropId);
SET @PropagationDays = (SELECT PropagationTime FROM CropPropagationAttributes WHERE CropId=@SpringOnionCropId);
SET @PropRemovalDate = DateAdd(dd,-1,@BedPlantDate);
SET @PropPlantDate = DateAdd(dd,-@PropagationDays, @PropRemovalDate);

--Propagation
INSERT INTO ModuleTrays (AreaId, TrayTypeId, CropId, SeedsPerModule, PlantingDate, RemovalDate, BedCropId)
VALUES
(@AreaHeatedTableId, @ModuleTrayStandard60Id, @SpringOnionCropId, @SeedsPerUnit, @PropPlantDate, @PropRemovalDate, @GardenBlock1Bed1); -- spring onion

--Bed planting
INSERT INTO BedCrops (BedId, CropId, PlantingDate, RemovalDate)
VALUES
(XXX, @SpringOnionCropId, @BedPlantDate, @BedRemovalDate); -- spring onion in Bed XXX


--Lettuce mix.  Little Gem, Saragossa, Valmaine, Lollo Rossa and Bijou,



