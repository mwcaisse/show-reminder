import { SavedEpisode } from "./saved-episode";


export class SavedShow {
    tvdbId: number;
    name: string;
    firstAiredDate: Date;
    airDay: string;
    airTime: string;
    lastEpisodeId: number;
    nextEpisodeId: number;

    lastEpisode: SavedEpisode;
    nextEpisode: SavedEpisode;

}