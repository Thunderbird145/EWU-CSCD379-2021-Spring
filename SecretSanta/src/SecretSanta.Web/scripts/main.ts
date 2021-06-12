import '../styles/site.css';

import 'alpinejs';
import axios from 'axios';

import { library, dom } from "@fortawesome/fontawesome-svg-core";
import { fas, faThList } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';

import { Group, GroupsClient, User, UsersClient, Gift, GiftsClient } from '../Api/SecretSanta.Api.Client.g';

library.add(fas, far, fab);
dom.watch();

declare var apiHost : string;

export function setupNav() {
    return {
        toggleMenu() {
            var headerNav = document.getElementById('headerNav');
            if (headerNav) {
                if (headerNav.classList.contains('hidden')) {
                    headerNav.classList.remove('hidden');
                } else {
                    headerNav.classList.add('hidden');
                }
            }
        }
    }
}

export function setupGifts() {
    return {
        gifts: [] as Gift[],
        async mounted() {
            await this.loadGifts(); 
        },
        async deleteGift(currentGift: Gift) {
            if (confirm(`Are you sure you want to delete ${currentGift.title}?`)) {
                var client = new GiftsClient(apiHost);
                await client.delete(currentGift.id);
                await this.loadGifts();
            }
        },
        async loadGifts() {
            try {
                var client = new GiftsClient(`${apiHost}`);
                this.gifts = await client.getAll() || [];
            } catch (error) {
                console.log(error);
            }
        }
    }
}

export function createOrUpdateGift() {
    return {
        gift: {} as Gift,
        gifts: [] as Gift[],
        selectedGiftId: 0,
        isEditing: false,
        async create() {
            try {
                const pathnameSplit = window.location.pathname.split('/');
                const userId = pathnameSplit[pathnameSplit.length - 1];
                const giftclient = new GiftsClient(apiHost);
                this.gift.userId = parseInt(userId);
                await giftclient.post(this.gift);
                window.location.href='/users/edit/' + this.gift.userId;
            } catch (error) {
                console.log(error);
            }
        },
        async update() {
            try {
                const client = new GiftsClient(apiHost);
                await client.put(this.gift.id, this.gift);
                window.location.href='/users/edit/' + this.gift.userId;
            } catch (error) {
                console.log(error);
            }
        },
        async loadData() {
            this.loadGift();
        },
        async loadGift() { 
            const pathnameSplit = window.location.pathname.split('/');
            const id = pathnameSplit[pathnameSplit.length - 1];
            try {
                const client = new GiftsClient(apiHost);
                this.gift = await client.get(+id);
            } catch (error) {
                console.log(error);
            }
        }
    }
}

export function setupUsers() {
    return {
        users: [] as User[],
        async mounted() {
            await this.loadUsers();
        },
        async deleteUser(currentUser: User) {
            if (confirm(`Are you sure you want to delete ${currentUser.firstName} ${currentUser.lastName}?`)) {
                var client = new UsersClient(apiHost);
                await client.delete(currentUser.id);
                await this.loadUsers();
            }
        },
        async loadUsers() {
            try {
                var client = new UsersClient(`${apiHost}`);
                this.users = await client.getAll() || [];
            } catch (error) {
                console.log(error);
            }
        }
    }
}

export function createOrUpdateUser() {
    return {
        user: {} as User,
        allGifts: [] as Gift[],
        selectedGiftId: 0,
        isEditing: false,
        async create() {
            try {
                const client = new UsersClient(apiHost);
                await client.post(this.user);
                window.location.href='/users';
            } catch (error) {
                console.log(error);
            }
            this.loadData;
        },
        edit() {
            this.isEditing = true;
        },
        async update() {
            try {
                const client = new UsersClient(apiHost);
                await client.put(this.user.id, this.user);
                this.isEditing = false;
            } catch (error) {
                console.log(error);
            }
            this.loadData;
        },
        async loadData() {
            this.loadUser();
            this.loadGifts();
        },
        async loadUser() { 
            const pathnameSplit = window.location.pathname.split('/');
            const id = pathnameSplit[pathnameSplit.length - 1];
            try {
                const client = new UsersClient(apiHost);
                this.user = await client.get(+id);
            } catch (error) {
                console.log(error);
            }
        },
        async loadGifts() {
            try {
                var client = new GiftsClient(apiHost);
                this.allGifts = await client.getAll() || [];
            } catch (error) {
                console.log(error);
            }
        },
        async removeFromUser(currentUser: User, gift: Gift) {
            if (confirm(`Are you sure you want to remove ${gift.title} from ${currentUser.firstName}'s list?`)) {
                try {
                    var client2 = new GiftsClient(apiHost);
                    await client2.delete(gift.id);
                } catch (error) {
                    console.log(error);
                }
                await this.loadUser();
            }
        },
        async createGift(currentUser: User) {
            try {
                window.location.href='/gifts/create/' + this.user.id;
                console.log("creating gift");
            } catch (error) {
                console.log(error);
            }
        }
    }
}

export function setupGroups() {
    return {
        groups: [] as Group[],
        allGifts: [] as Gift[],
        selectedUserId: 0,
        isEditing: false,
        generationError: "",

        async mounted() {
            await this.loadGroups();
        },
        async deleteGroup(currentGroup: Group) {
            if (confirm(`Are you sure you want to delete ${currentGroup.name}?`)) {
                var client = new GroupsClient(apiHost);
                await client.delete(currentGroup.id);
                await this.loadGroups();
            }
        },
        async loadGroups() {
            try {
                var client = new GroupsClient(`${apiHost}`);
                this.groups = await client.getAll() || [];
            } catch (error) {
                console.log(error);
            }
        }
    }
}

export function createOrUpdateGroup() {
    return {
        group: {} as Group,
        allUsers: [] as User[],
        selectedUserId: 0,
        isEditing: false,
        generationError: "",
        async create() {
            try {
                const client = new GroupsClient(apiHost);
                await client.post(this.group);
                window.location.href = '/groups';
            } catch (error) {
                console.log(error);
            }
        },
        edit() {
            this.isEditing = true;
        },
        async update() {
            try {
                const client = new GroupsClient(apiHost);
                await client.put(this.group.id, this.group);
                this.isEditing = false;
                await this.loadGroup();
            } catch (error) {
                console.log(error);
            }
        },
        async loadData() {
            await this.loadGroup();
            await this.loadUsers();
        },
        async loadGroup() {
            const pathnameSplit = window.location.pathname.split('/');
            const id = pathnameSplit[pathnameSplit.length - 1];
            try {
                const client = new GroupsClient(apiHost);
                this.group = await client.get(+id);
            } catch (error) {
                console.log(error);
            }
        },
        async loadUsers() {
            try {
                var client = new UsersClient(apiHost);
                this.allUsers = await client.getAll() || [];
                var index = this.allUsers.findIndex(x => true);
                if (index >= 0) {
                    this.selectedUserId = this.allUsers[index].id;
                }
            } catch (error) {
                console.log(error);
            }
        },
        async removeFromGroup(currentGroup: Group, user: User) {
            if (confirm(`Are you sure you want to remove ${user.firstName} ${user.lastName} from ${currentGroup.name}?`)) {
                try {
                    var client = new GroupsClient(apiHost);
                    await client.remove(currentGroup.id, user.id);
                } catch (error) {
                    console.log(error);
                }
                await this.loadGroup();
            }
        },
        async addToGroup(currentGroupId: number) {
            if (this.selectedUserId <= 0) return;
            try {
                var client = new GroupsClient(apiHost);
                await client.add(currentGroupId, this.selectedUserId);
            } catch (error) {
                console.log(error);
            }
            await this.loadGroup();
        },
        async generateAssignments(currentGroupId: number) {
            this.generationError = "";
            try {
                var client = new GroupsClient(apiHost);
                await client.createAssignments(currentGroupId);
            } catch (error) {
                console.log(error);
                this.generationError = error;
            }
            await this.loadGroup();
        },
        getAssignment(user: User): string {
            if (user) {
                var assignment = this.group.assignments.find(x => x.giver?.id == user.id);
                if (assignment) {
                    return assignment.receiver?.firstName + " " + assignment.receiver?.lastName;
                }
            }
            return "";
        }
    }
}