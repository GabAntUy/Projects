USE FoodInspections
GO


--1)Creación de índices que considere puedan ser útiles para optimizar las consultas (según criterio establecido en el curso).

--De acuerdo a criterio visto en clase serían las fk de las tablas:

--Tabla Licencias
CREATE INDEX I_estNumero_Licencias ON Licencias(estNumero)

--Tabla Inspecciones
CREATE INDEX I_estNumero ON Inspecciones(estNumero)
CREATE INDEX I_violCodigo ON Inspecciones(violCodigo)

--Se agregan estos dos índices extra porque podrían ser útiles para mejorar la performance de consultas en la bd:
--Los siguientes índices estarían justificados si el balance de las operaciones de escritura y lectura en la práctica
--demuestra que las mejoras en el tiempo de consulta compensan el costo adicional en las operaciones de escritura de la base de datos.

CREATE INDEX I_estNumero_inspFecha_inspResultado ON Inspecciones(estNumero, inspFecha, inspResultado)
--Este índice aprovecha la combinación de estNumero y inspFecha para la identificación de inspecciones y acceder a su resultado.
--También sería útil para consultas que combinan filtros sobre establecimiento, fecha y resultado de inspección.


CREATE INDEX I_licFchVto_licStatus ON Licencias(licFchVto, licStatus)
--Este índice podría ser beneficiosa para las consultas que filtran las licencias por su estado y su fecha de vencimiento.




--2)Ingreso de un juego completo de datos de prueba (será más valorada la calidad de los datos que la cantidad). 

INSERT INTO Establecimientos (estNombre, estDireccion, estTelefono, estLatitud, estLongitud)
VALUES 
('Los Yuyos', 'Av. Luis Alberto de Herrera 4297', '23363069', -34.8590, -56.1906),--1
('Club de la Papa Frita', 'Bv. Gral. Artigas 3654', '22081511', -34.8702, -56.1824),--2
('Bar Sporting', '21 de Setiembre 2435','27106563',-34.9131, -56.1623),--3
('Bar Los 4','Carlos Berg 2550','27124052',-34.9136, -56.15784),--4
('Tasende','Ciudadela 1300','29003504',-34.9075, -56.1994),--5
('Parrillada Modelo','Av. Luis Alberto de Herrera 3083','24821525',-34.8733, -56.1607),--6
('McDonald’s','Av. Luis Alberto de Herrera 1290','26282539',-34.9035, -56.1373),--7
('Pizzería Rodelu','Av. Luis Alberto de Herrera 1172','26280996',-34.9062, -56.1363),--8
('El Mesón Español','Av. 18 de Julio 1332','29015145', -34.9058, -56.1871),--9
('El Fogón','San José 1080','29000900',-34.9071, -56.1929),--10
--No tuvo inspecciones
('Burger King','Arenal Grande 2246','095845307', -34.8875, -56.1806),--11
--Tuvo una sola inspección con resultado 'Oficina no encontrada' (no fue aprobada ni reprobada de acuerdo a interpretación de la letra)
('Bar Fraternidad','Av. Gral. Flores 3724','22153293', -34.8612, -56.1673);--12



--Se asume que todos los establecimientos tienen una o más licencias. 
--En caso de renovación se asume que las licencias son emitidas luego de que la anterior ya venció.
--Por practicidad para el manejo de datos de prueba las primeras licencias de cada establecimiento
--tienen fecha de emisión desde 2020 en adelante. Y la duración de las licencias es de 1 año.
--Se decidió asignar 'REV' a aquellas que en la última inspección tuvieron resultado 'Falla'o que estan vencidas.

SET DATEFORMAT DMY
INSERT INTO Licencias (estNumero, licFchEmision, licFchVto, licStatus)
VALUES
(1,'01-01-2020','01-01-2021','REV'),--1
(1,'02-02-2021','02-02-2022','REV'),--2
(1,'03-02-2022','03-02-2023','REV'),--3
(1,'25-06-2023','25-06-2024','REV'),--4
--
(2,'05-05-2023','05-05-2024','REV'),--5
(2,'07-05-2024','07-05-2025','APR'),--6
--
(3,'10-06-2022','10-06-2023','APR'),--7
(3,'29-06-2023','29-06-2024','APR'),--8
--
(4,'25-08-2022','25-08-2023','REV'),--9
(4,'29-08-2023','29-08-2024','APR'),--10
--
(5,'20-09-2023','20-09-2024','APR'),--11
--
(6,'01-06-2022','01-06-2023','REV'),--12
--
(7,'30-07-2023','30-07-2024','APR'),--13
--
(8,'25-07-2023','25-07-2024','APR'),--14
--
(9,'28-09-2023','28-09-2024','APR'),--15
--
(10,'28-09-2023','28-09-2024','APR'),--16
--
(11,'28-09-2019','28-09-2020','REV'),--17
(11,'28-10-2020','28-10-2021','REV'),--18
(11,'30-10-2023','30-10-2024','APR'),--19
--
(12,'05-08-2023','05-08-2024','APR');--20


INSERT INTO TipoViolacion (violDescrip) 
VALUES 
('Sin violación'),--1
('Higiene deficiente'),--2
('Almac. inadecuado alimentos'),--3
('Uso indebido de químicos'),--4
('Personal sin exámenes médicos'),--5
('Presencia de plagas o animales'),--6
('Sin de extintores de incendio'),--7
('Ventilación insuficiente'),--8
('Alimentos vencidos');--9



--Para simplificar la corrección de las consultas se ingresó solo la fecha en el campo inspFecha que es DATETIME.
--Debido a la estructura dada de la base de datos, para registrar múltiples violaciones de un establecimiento en una misma inspección, 
--es necesario ingresar varias filas en la tabla Inspecciones con la misma fecha de inspección (inspFecha). 
--Cada fila representará una violación distinta detectada durante la inspección, compartiendo la misma inspFecha y estNumero, pero variando en el código de violación (violCodigo). 
--Así, aunque se utilicen múltiples filas, todos corresponden a una única inspección realizada en una fecha(inspFecha) específica.
--Da a lugar a que una misma inspección este almacenada en varias filas con distinta pk(inspID). 
--Se sabrán que pertenecen a una misma inspección porque tendran la misma inspFecha y mismo estNumero.
--Para una inspección se interpreta que si en una de sus filas se registra un inspResultado 'Falla', 
--el resto de sus filas también van a tener el valor 'Falla' en el campo inspResultado para que tenga sentido un resultado único por inspección.

SET DATEFORMAT DMY;
INSERT INTO Inspecciones (inspFecha, estNumero, inspRiesgo, inspResultado, violCodigo, inspComents)
VALUES
--Cometieron todos los tipos de violación(sin contar la 1 'Sin violación')
('15-06-2024', 1, 'Alto', 'Falla', 2, 'Higiene extremadamente deficiente en todas las áreas.'),
('15-06-2024', 1, 'Alto', 'Falla', 3, 'Alimentos almacenados a temperaturas inseguras.'),
('15-06-2024', 1, 'Alto', 'Falla', 4, 'Uso indebido y peligroso de químicos en cocina.'),
('15-06-2024', 1, 'Alto', 'Falla', 5, 'Personal sin exámenes médicos.'),
('15-06-2024', 1, 'Alto', 'Falla', 6, 'Presencia de roedores en el almacenamiento.'),
('15-06-2024', 1, 'Alto', 'Falla', 7, 'Falta de extintores de incendio.'),
('15-06-2024', 1, 'Alto', 'Falla', 8, 'Insuficiente ventilación en la cocina.'),
('15-06-2024', 1, 'Alto', 'Falla', 9, 'Se encontraron alimentos vencidos durante la inspección.'),
--
('15-06-2024', 2, 'Alto', 'Falla', 2, 'Condiciones de higiene deplorables encontradas.'),
('15-06-2024', 2, 'Alto', 'Falla', 3, 'Inadecuado almacenamiento frío para alimentos críticos.'),
('15-06-2024', 2, 'Alto', 'Falla', 4, 'Manejo incorrecto de productos químicos en áreas de preparación.'),
('15-06-2024', 2, 'Alto', 'Falla', 5, 'Ausencia de certificados médicos en personal.'),
('15-06-2024', 2, 'Alto', 'Falla', 6, 'Infestación de insectos en el área de comedor.'),
('15-06-2024', 2, 'Alto', 'Falla', 7, 'No se encontraron extintores en áreas clave.'),
('15-06-2024', 2, 'Alto', 'Falla', 8, 'Problemas de ventilación en áreas cerradas.'),
('15-06-2024', 2, 'Alto', 'Falla', 9, 'Venta de alimentos caducados detectada.'),
--
--Con fechas más reciente
('17-06-2024', 9, 'Alto', 'Falla', 3, 'Alimentos almacenados a temperatura inadecuada.'),
('17-06-2024', 9, 'Alto', 'Falla', 5, 'Personal sin exámenes médicos actualizados.'),
('17-06-2024', 1, 'Alto', 'Falla', 2, 'Higiene muy deficiente en la cocina.'),
('17-06-2024', 2, 'Alto', 'Falla', 3, 'Alimentos almacenados a temperatura inadecuada.'),
('19-06-2024', 5, 'Bajo', 'Pasa', 1, 'Todo en condiciones.'),
--
('10-06-2024', 6, 'Alto', 'Falla', 7, 'Falta de extintores en zonas críticas.'),
('15-06-2024', 4, 'Alto', 'Falla', 5, 'Personal sin exámenes médicos actualizados.'),
('15-06-2024', 6, 'Medio', 'Pasa con condiciones', 3, 'Necesita mejorar el almacenamiento de alimentos.'),
('02-05-2024', 3, 'Bajo', 'Pasa con condiciones', 4, 'Deberá mejorar el manejo de químicos.'),
('08-03-2024', 5, 'Medio', 'Pasa con condiciones', 6, 'Presencia de animales en el almacén.'),
--En año no actual
('25-06-2020', 9, 'Medio', 'Pasa con condiciones', 3, 'Necesita mejorar el almacenamiento de alimentos.'),
('02-05-2023', 6, 'Bajo', 'Pasa con condiciones', 4, 'Deberá mejorar el manejo de químicos.'),
('08-03-2022', 4, 'Medio', 'Pasa con condiciones', 6, 'Presencia de animales en el almacén.'),
('22-11-2021', 3, 'Medio', 'Pasa con condiciones', 2, 'Limpieza insuficiente en áreas de servicio.'),
('11-05-2023', 1, 'Medio', 'Pasa con condiciones', 4, 'Uso adecuado de químicos, pero requiere vigilancia continua.'),
('22-11-2022', 8, 'Medio', 'Pasa con condiciones', 2, 'Limpieza insuficiente en áreas de servicio.'),
('11-05-2023', 10, 'Medio', 'Pasa con condiciones', 4, 'Uso adecuado de químicos, pero requiere vigilancia continua.'),
('08-06-2023', 5, 'Bajo', 'Pasa', 1, 'No se detectaron violaciones.'),
('15-09-2020', 5, 'Bajo', 'Pasa', 1, 'No se detectaron violaciones.'),
('10-06-2023', 5, 'Bajo', 'Pasa', 1, 'No se detectaron violaciones.'),
('10-02-2023', 5, 'Bajo', 'Pasa', 1, 'No se detectaron violaciones.'),
('07-08-2022', 3, 'Bajo', 'Pasa', 1, 'No se detectaron violaciones.'),
('06-04-2021', 6, 'Bajo', 'Pasa', 1, 'No se detectaron violaciones.'),
('10-06-2023', 1, 'Bajo', 'Pasa', 1, 'No se detectaron violaciones.'),
('10-06-2022', 1, 'Bajo', 'Pasa', 1, 'No se detectaron violaciones.'),
('19-04-2021', 1, 'Bajo', 'Pasa', 1, ''),
('08-11-2021', 7, 'Bajo', 'Pasa', 1, NULL),
--Con resultado 'Oficina no encontrada'(ni aprobada ni reprobada de acuerdo a interpretación de letra)
('19-04-2021', 12, 'Bajo', 'Oficina no encontrada', 1, 'No se encontró el establecimiento.');
--




--3)Utilizando SQL implementar las siguientes consultas:

--a)Mostrar nombre, dirección y teléfono de los establecimientos que tuvieron la inspección fallida más reciente.

--Se interpreta la letra como que la consulta debería devolver datos de establecimientos que tuvieron fallida la inspección
--en la fecha más reciente que hubo una una inspección con resultado 'Falla'.
--En otras palabras, si la inspección fallida más reciente en la base de datos ocurrió el 17/6/2024,
--la consulta devolverá los detalles de los establecimientos que fallaron una inspección en esa fecha.
--(Se usa DISTINCT porque en caso de múltiples violaciones en una inspección, esta generaría más de una fila en la tabla Inspecciones, y se repetiría el establecimiento en el resultado de la consulta)


SELECT DISTINCT e.estNombre AS Nombre, e.estDireccion AS Direccion, e.estTelefono As Telefono
FROM Establecimientos e
JOIN Inspecciones i ON e.estNumero = i.estNumero
WHERE i.inspResultado = 'Falla'
AND i.inspFecha = (SELECT MAX(inspFecha) 
                   FROM Inspecciones
                   WHERE inspResultado = 'Falla');


--Resultado esperado:
--Club de la Papa Frita	Bv. Gral. Artigas 3654	22081511
--El Mesón Español	Av. 18 de Julio 1332	29015145
--Los Yuyos	Av. Luis Alberto de Herrera 4297	23363069




--b)Mostrar los 5 tipos de violaciones mas comunes, 
--el informe debe mostrar código y descripción de la violación y cantidad de inspecciones en el año presente.

--De acuerdo a como se interpretó la letra la consulta debe devolver para los 5 tipos de violaciones más comunes(históricas, no de este año)
--código y descripción de la violación y cantidad de inspecciones en el año presente agrupadas por tipo de violación.
--(Se utilizó una subconsulta en el FROM para obtener la tabla de 5 más comunes históricas incluyendo empates)


SELECT t.violCodigo AS Codigo_Violacion, t.violDescrip AS Descripcion, COUNT(*) AS Cantidad_Inspecciones_Anio_Actual
FROM Inspecciones i, TipoViolacion t, (SELECT TOP 5 WITH TIES ii.violCodigo, COUNT(*) AS Cant 
									   FROM Inspecciones ii 
									   WHERE ii.violCodigo <> 1--se excluye violCodigo 1 que es 'Sin violación'.
									   GROUP BY  ii.violCodigo
									   ORDER BY Cant DESC) AS Mas_Comunes
WHERE i.violCodigo = t.violCodigo AND Mas_Comunes.violCodigo=t.violCodigo AND YEAR(i.inspFecha) = YEAR(GETDATE())
GROUP BY t.violCodigo, t.violDescrip
ORDER BY Cantidad_Inspecciones_Anio_Actual DESC
    
--Resultado esperado:
--3	Almac. inadecuado alimentos	5
--5	Personal sin exámenes médicos	4
--6	Presencia de plagas o animales	3
--4	Uso indebido de químicos	3
--2	Higiene deficiente	3




--c)Mostrar número y nombre de los establecimientos que cometieron todos los tipos de violación que existen.

--En la consulta se excluye el tipo de violación violCodigo 1 que es 'Sin violación'.

SELECT  e.estNumero AS Numero, e.estNombre AS Nombre
FROM Inspecciones i, Establecimientos e
WHERE  e.estNumero=i.estNumero AND i.violCodigo <>1--se excluye violCodigo 1 que es 'Sin violación'.
GROUP BY e.estNumero, e.estNombre 
HAVING  COUNT(DISTINCT violCodigo) = (SELECT COUNT(*) FROM TipoViolacion t WHERE  t.violCodigo <>1)--se excluye violCodigo 1 que es 'Sin violación'.



--Resultado esperado:
--1	Los Yuyos
--2	Club de la Papa Frita




--d)Mostrar el porcentaje de inspecciones reprobadas por cada establecimiento, 
--incluir dentro de la reprobación las categorías 'Falla', 'Pasa con condiciones'.

--Se interpretó la consulta como mostrar el porcentaje de reprobación(tomando como reprobadas las que tienen inspResultado:'Falla' o 'Pasa con condiciones') de inspecciones para cada establecimiento.
--Para enriquecer la consulta se hizo un LEFT JOIN para mostar mostrar los establecimientos que no recibieron inspecciones.
--El tener establecimientos con 0 inspecciones llevó a usar NULLIF para no dividir por 0 y COALESCE para manejar los NULL que devuelve NULLIF 
--cuando el total de inspecciones de un establecimiento es 0.
--Se agregó en la consulta la columna Total_Inspecciones e Inspecciones_Reprobadas para darle más contexto al porcentaje de reprobaciones 
--y facilitar la correción de la consulta.

SELECT  
    e.estNumero AS Numero_Establecimiento, 
    e.estNombre AS Nombre,  
    COUNT(DISTINCT i.inspFecha) AS Total_Inspecciones,
    COUNT(DISTINCT CASE WHEN i.inspResultado IN ('Falla', 'Pasa con condiciones') THEN i.inspFecha ELSE NULL END) AS Inspecciones_Reprobadas,
    CAST(COALESCE((COUNT(DISTINCT CASE WHEN i.inspResultado IN ('Falla', 'Pasa con condiciones') THEN i.inspFecha ELSE NULL END) * 100 / NULLIF(COUNT(DISTINCT i.inspFecha), 0)), 0) AS INT) AS Porcentaje_Reprobacion
FROM Establecimientos e
LEFT JOIN Inspecciones i ON i.estNumero = e.estNumero
GROUP BY e.estNombre, e.estNumero
ORDER BY Porcentaje_Reprobacion DESC, Total_Inspecciones DESC

--Resultado esperado:
--2	Club de la Papa Frita	2	2	100
--4	Bar Los 4	2	2	100
--9	El Mesón Español	2	2	100
--10	El Fogón	1	1	100
--8	Pizzería Rodelu	1	1	100
--6	Parrillada Modelo	4	3	75
--3	Bar Sporting	3	2	66
--1	Los Yuyos	6	3	50
--5	Tasende	6	1	16
--7	McDonald’s	1	0	0
--12	Bar Fraternidad	1	0	0
--11	Burger King	0	0	0




--e)Mostrar el ranking de inspecciones de establecimientos, dicho ranking debe mostrar número y nombre del establecimiento,
--total de inspecciones, total de inspecciones aprobadas ('Pasa'), porcentaje de dichas inspecciones aprobadas, 
--total de inspecciones reprobadas ('Falla', 'Pasa con condiciones') y porcentaje de dichas inspecciones reprobadas, 
--solo tener en cuenta establecimientos cuyo status de licencia es APR.	

--Al igual que en la consulta anterior se usa LEFT JOIN para mostrar todos los establecimientos incluyendo los que no recibieron inspecciones.
--Puede aparecer en la consulta casos con inspecciones que no se reflejen en aprobadas ni reprobadas ya que el resultado de su inpección fue 'Oficina no encontrada'

SELECT  
    e.estNumero AS Numero_Establecimiento, 
    e.estNombre AS Nombre,  
    COUNT(DISTINCT i.inspFecha) AS Total_Inspecciones,
    COUNT(DISTINCT CASE WHEN i.inspResultado IN ('Pasa') THEN i.inspFecha ELSE NULL END) AS Total_Aprobadas,
	CAST(COALESCE((COUNT(DISTINCT CASE WHEN i.inspResultado IN ('Pasa') THEN i.inspFecha ELSE NULL END) * 100 / NULLIF(COUNT(DISTINCT i.inspFecha), 0)), 0) AS INT) AS Porcentaje_Aprobadas,
    COUNT(DISTINCT CASE WHEN i.inspResultado IN ('Falla', 'Pasa con condiciones') THEN i.inspFecha ELSE NULL END) AS Total_Reprobadas,
    CAST(COALESCE((COUNT(DISTINCT CASE WHEN i.inspResultado IN ('Falla', 'Pasa con condiciones') THEN i.inspFecha ELSE NULL END) * 100 / NULLIF(COUNT(DISTINCT i.inspFecha), 0)), 0) AS INT) AS Porcentaje_Reprobadas
FROM Establecimientos e
LEFT JOIN Inspecciones i ON i.estNumero = e.estNumero
JOIN Licencias l ON e.estNumero=l.estNumero
WHERE l.licStatus = 'APR'
GROUP BY e.estNombre, e.estNumero


--Resultado esperado:
--2	Club de la Papa Frita	2	0	0	2	100
--3	Bar Sporting	3	1	33	2	66
--4	Bar Los 4	2	0	0	2	100
--5	Tasende	6	5	83	1	16
--7	McDonald’s	1	1	100	0	0
--8	Pizzería Rodelu	1	0	0	1	100
--9	El Mesón Español	2	0	0	2	100
--10	El Fogón	1	0	0	1	100
--11	Burger King	0	0	0	0	0
--12	Bar Fraternidad	1	0	0	0	0




--f)Mostrar el tiempo promedio que tarda cada establecimiento en renovar su licencia.(la letra fue cambiada pero se consultó con profesor para dejar esta consulta que era la original)

--Se interpreta la consulta como la cantidad de días promedio que tarda un establecimiento en renovar la licencia.
-- Se hace uso de la función LEAD junto con PARTITION BY para obtener la próxima fecha de emisión.
-- LEAD mira hacia adelante en las filas siguientes para obtener el valor de licFchEmision
-- de la fila que está directamente después de la fila actual dentro de cada PARTITION (agrupada por estNumero)
-- y ordenada de forma ascendente respecto a la fecha de emisión.
--El uso de WHERE Proxima_FchEmision IS NOT NULL es para no mostrar en la tabla los establecimientos que solo tuvieron una licencia(si no aparecerían con NULL en el campo Promedio_Dias_Renovacion)

SELECT estNumero AS Numero_Establecimiento, AVG(DATEDIFF(day, licFchVto, Proxima_FchEmision)) AS Promedio_Dias_Renovacion
FROM(SELECT estNumero, licFchVto, LEAD(licFchEmision) OVER (PARTITION BY estNumero ORDER BY licFchEmision) AS Proxima_FchEmision
     FROM Licencias) AS Licencias_Con_Proxima_Emision
WHERE Proxima_FchEmision IS NOT NULL      
GROUP BY estNumero;  


--Resultado esperado:
--1	58
--2	2
--3	19
--4	4
--11	381





--6)Escribir una vista que muestre todos los datos de las licencias vigentes y los días que faltan para el vencimiento de cada una de ellas.

--Se interpreta como licencias vigentes las que tienen fecha de vencimiento mayor a la fecha de hoy y tienen licStatus = 'APR'
--Al convertir GETDATE() a tipo DATE usando CONVERT(DATE, GETDATE()), se elimina el componente de tiempo (hora, minutos, segundos),
--dejando solo la fecha para que la comparación sea entre fechas.

CREATE VIEW Vista_Licencias_Vigente AS
SELECT licNumero AS Numero_Licencia,
       estNumero AS Numero_Establecimiento,
       licFchEmision AS Fecha_Emision,
       licFchVto AS Fecha_Vencimiento,
       licStatus,
	   DATEDIFF(DAY,(CONVERT(DATE, GETDATE())),l.licFchVto) Dias_Para_Vencer
FROM Licencias l
WHERE l.licFchVto > CONVERT(DATE, GETDATE()) AND l.licStatus ='APR'

SELECT *
from Vista_Licencias_Vigente




--5)Escribir los siguientes disparadore:

--a)Cada vez que se crea un nuevo establecimiento, se debe crear una licencia de aprobación con vencimiento 90 días,
--el disparador debe ser escrito teniendo en cuenta la posibilidad de ingresos múltiples.

CREATE TRIGGER trg_Auto_Licencia ON Establecimientos AFTER INSERT
AS
BEGIN
	DECLARE @hoy DATE = CONVERT(DATE, GETDATE());

	INSERT INTO Licencias(estNumero, licFchEmision, licFchVto, licStatus) SELECT i.estNumero,  @hoy, DATEADD(DAY, 90,  @hoy) , 'APR' FROM inserted i

END

--Script para facilitar correción

--select * from Establecimientos
--select * from Licencias

--INSERT INTO Establecimientos (estNombre, estDireccion, estTelefono, estLatitud, estLongitud)
--VALUES 
--('Los Yuyos 2', 'Av. Luis Alberto de Herrera 4297', '23363069', -34.8590, -56.1906),
--('Club de la Papa Frita 2', 'Bv. Gral. Artigas 3654', '22081511', -34.8702, -56.1824),
--('Bar Sporting 2', '21 de Setiembre 2435','27106563',-34.9131, -56.1623)

--select DATEDIFF(day,'Ingresar licFchEmision','Ingresar licFchVto')




--b)No permitir que se ingresen inspecciones de establecimientos cuya licencia está próxima a vencer,
--se entiende por próxima a vencer a todas aquellas cuyo vencimiento esté dentro de los siguientes 5 días,
 --el disparador debe tener en cuenta la posibilidad de registros múltiples.

 
 --'Próximos 5 días' incluye todas las fechas desde hoy (inclusive) hasta hoy más 5 días (inclusive).
 --Se implementó una solución con tipo de dato DATE en vez de DATETIME para simplificar la correción.

 CREATE TRIGGER trg_Validacion_Inspecciones ON Inspecciones INSTEAD OF INSERT
 AS
 BEGIN
	
		DECLARE @hoy DATE = CONVERT(DATE, GETDATE());
		DECLARE @cincoDias DATE = DATEADD(DAY, 5, @hoy);
		DECLARE @estVenciendo TABLE (estNumero INT);

		INSERT INTO @estVenciendo(estNumero)
        SELECT l.estNumero
        FROM Licencias l
        GROUP BY l.estNumero
        HAVING MAX(l.licFchVto) BETWEEN @hoy AND @cincoDias;

		INSERT INTO Inspecciones(inspFecha, estNumero, inspRiesgo, inspResultado, violCodigo, inspComents)
		SELECT i.inspFecha, i.estNumero, i.inspRiesgo, i.inspResultado, i.violCodigo, i.inspComents		
		FROM inserted i		
		WHERE i.estNumero NOT IN (SELECT estNumero FROM @estVenciendo);

	   IF EXISTS(SELECT * 
			     FROM  inserted i 
			     WHERE i.estNumero  IN (SELECT estNumero FROM @estVenciendo))
			 
	   BEGIN
		
		  DECLARE @msg VARCHAR(1000)=''

		  SELECT @msg = @msg + 'estNumero: ' + CONVERT(VARCHAR, i.estNumero) + ', Fecha de vencimiento de licencia: ' + CONVERT(VARCHAR, MAX(l.licFchVto)) + '; '
		  FROM inserted i
		  JOIN Licencias l ON i.estNumero = l.estNumero
		  WHERE i.estNumero IN (SELECT estNumero FROM @EstVenciendo)
		  GROUP BY i.estNumero;

		  PRINT 'Las inspecciones con los estNumero indicados abajo no fueron insertadas (sus licencias vencen en los próximos 5 días)';
		  PRINT @msg

	   END
END


--Script para facilitar correción (con datos de prueba extra para ingresar)


--select * 
--from Licencias l
--where l.estNumero in (1,2,3)

--delete
--from Licencias 
--where estNumero in (1,2,3)

--SET DATEFORMAT DMY
--INSERT INTO Licencias (estNumero, licFchEmision, licFchVto, licStatus)
--VALUES
--(1,'01-01-2020',DATEADD(DAY,0,CONVERT(DATE,GETDATE())),'APR'),
--(2,'01-01-2020',DATEADD(DAY,5,CONVERT(DATE,GETDATE())),'APR'),
--(3,'01-01-2020',DATEADD(DAY,6,CONVERT(DATE,GETDATE())),'APR');


--SET DATEFORMAT DMY;
--INSERT INTO Inspecciones (inspFecha, estNumero, inspRiesgo, inspResultado, violCodigo, inspComents)
--VALUES
--('15-06-2024', 1, 'Alto', 'Falla', 1, 'NO DEBERIA INSERTARSE.'),
--('15-06-2024', 1, 'Alto', 'Falla', 1, 'NO DEBERIA INSERTARSE.'),
--('15-06-2024', 2, 'Alto', 'Falla', 1, 'NO DEBERIA INSERTARSE.'),
--('15-06-2024', 3, 'Alto', 'Falla', 1, 'DEBERIA INSERTARSE.');

--select * 
--from Inspecciones

--delete from Inspecciones where inspFecha = '15-06-2024'



--4)Utilizando T-SQL realizar los siguientes ejercicios: 

--a)Escribir un procedimiento almacenado que dado un tipo de riesgo ('Bajo','Medio','Alto'),
--muestre los datos de las violaciones (violCodigo, violDescrip) para dicho tipo, no devolver datos repetidos.

CREATE PROCEDURE Riesgo_Violaciones @inspRiesgo VARCHAR(5)
AS
BEGIN

	SELECT  DISTINCT t.violCodigo AS Codigo_Violacion, violDescrip AS Descripcion
	FROM inspecciones i JOIN TipoViolacion t ON i.violCodigo = t.violCodigo
	WHERE i.inspRiesgo = @inspRiesgo
END

EXEC Riesgo_Violaciones 'Medio'




--b)Mediante una función que reciba un código de violación, 
--devolver cuantos establecimientos con licencia vencida y nunca renovada tuvieron dicha violación.

--Se interpreta 'licencia vencida y nunca renovada' como que tengan la licencia más reciente vencida.
--La parte de 'nunca renovada' se interpreta como que no hay licencias emitidas después de la última que ha vencido y no como que nunca haya renovado una licencia.
--Se asume que si la fecha de vencimiento más reciente(MAX(l.licFchVto)) está vencida, no hay licencias más recientes emitidas después de esta,
--ya que cualquier nueva licencia emitida tendría valor de fecha de vencimiento MAX(l.licFchVto).

CREATE FUNCTION Establecimientos_Lic_Vencida (@violCodigo INT) RETURNS INT
AS
BEGIN

 DECLARE @resultado INT;

    	SELECT @resultado = COUNT(*)
		FROM Establecimientos e
		WHERE e.estNumero IN (SELECT i.estNumero
							  FROM 	Inspecciones i
							  WHERE i.violCodigo = @violCodigo
							  )
			
		AND e.estNumero  IN (SELECT l.estNumero
							 FROM Licencias l
							 GROUP BY l.estNumero
							 HAVING MAX(l.licFchVto) <= CONVERT(DATE,GETDATE())
							 )
		RETURN @resultado;
END

SELECT dbo.Establecimientos_Lic_Vencida(1) AS Cantidad_Establecimientos





--c)Escribir un procedimiento almacenado que dado un rango de fechas,
--retorne por parámetros de salida la cantidad de inspecciones que tuvieron un resultado 'Oficina no encontrada' 
--y la cantidad de inspecciones que no tienen comentarios.

--Se interpreta 'no tienen comentarios' como valores NULL o '' en el campo inspComents.
--La logica del procedimiento contempla que se ingresen los valores de las fechas al revés(@fechaMin > @fechaMax).

CREATE or alter procedure Cantidad_Inspecciones @fechaMin DATE, @fechaMax DATE, @cantInspOficNoEncontrada INT OUTPUT ,@cantInspNoComentarios INT OUTPUT  
AS
BEGIN
	SET DATEFORMAT DMY;
	IF @fechaMin>@fechaMax
	BEGIN
		DECLARE @tempDate DATE;
		SET @tempDate = @fechaMin;
		SET @fechaMin = @fechaMax;
		SET @fechaMax = @tempDate;
	END

	BEGIN
		SELECT @cantInspOficNoEncontrada = COUNT(*)
		FROM Inspecciones i
		WHERE i.inspFecha BETWEEN @fechaMin AND @fechaMax AND i.inspResultado = 'Oficina no encontrada';

		SELECT @cantInspNoComentarios= COUNT(*)
		FROM Inspecciones i
		WHERE i.inspFecha BETWEEN @fechaMin AND @fechaMax AND (i.inspComents IS NULL OR i.inspComents = '');
	END
END

SET DATEFORMAT DMY;
DECLARE  @cantInspOficNoEncontrada INT;  
DECLARE @cantInspNoComentarios INT;
EXEC Cantidad_Inspecciones '19-04-2021','08-04-2022', @cantInspOficNoEncontrada OUTPUT,@cantInspNoComentarios OUTPUT;
PRINT 'Inspecciones con resultado -Oficina no encontrada-: ' + STR(@cantInspOficNoEncontrada);
PRINT 'Inspecciones sin comentarios: ' +STR(@cantInspNoComentarios);



