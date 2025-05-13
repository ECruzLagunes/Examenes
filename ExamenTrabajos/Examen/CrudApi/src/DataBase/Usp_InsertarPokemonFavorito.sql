CREATE PROCEDURE Usp_InsertarPokemonFavorito
    @IdUsuario INT,
    @IdPokemon INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO UsuariosPokemonFavorito (IdUsuario, PokemonId)
        VALUES (@IdUsuario, @IdPokemon);

        SET @Resultado = 1;
        SET @Mensaje = 'Pokémon favorito insertado correctamente.';

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SET @Resultado = -1;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END