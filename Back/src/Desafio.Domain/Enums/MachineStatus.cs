namespace Desafio.Domain.Enums
{
    /// <summary>
    /// Define os possíveis estados operacionais de uma máquina.
    /// Utilizado para representar o status atual da máquina ou sua telemetria.
    /// </summary>
    public enum MachineStatus
    {
        /// <summary>
        /// A máquina está em operação normal.
        /// </summary>
        Operating,

        /// <summary>
        /// A máquina está em manutenção preventiva ou corretiva.
        /// </summary>
        Maintenance,

        /// <summary>
        /// A máquina está desligada ou fora de operação.
        /// </summary>
        Shutdown
    }
}
